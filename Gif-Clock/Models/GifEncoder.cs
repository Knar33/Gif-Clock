using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GifClock
{
    public class GifEncoder
    {
        public Stream GifStream { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        private bool FirstFrame { get; set; }

        public GifEncoder(Stream inputStream, int width, int height)
        {
            Width = width;
            Height = height;
            GifStream = inputStream;
            FirstFrame = true;
        }

        private void GenerateHeader(MemoryStream firstFrameStream)
        {
            //Header Block
            Task.Run(() => WriteString("GIF89a")).Wait();

            //Logical Screen Descriptor
            WriteShort(Width); //Canvas Width
            WriteShort(Height); //Canvas Height
            firstFrameStream.Position = 10;
            WriteByte(firstFrameStream.ReadByte()); // Global Color Table Info
            WriteByte(0); //Background Color Index
            WriteByte(0); //Pixel Aspect Ratio
            WriteColorTable(firstFrameStream);
        }

        public async Task AddFrame(Image frame, int x, int y)
        {
            using (var sourceGif = new MemoryStream())
            {
                frame.Save(sourceGif, ImageFormat.Gif);
                if (FirstFrame)
                {
                    GenerateHeader(sourceGif);
                    FirstFrame = false;
                }
                sourceGif.Position = 789; //Position of the Image Descriptor
                var header = new byte[11];
                sourceGif.Read(header, 0, header.Length);
                WriteByte(header[0]);
                WriteShort(x);
                WriteShort(y);
                WriteShort(frame.Width);
                WriteShort(frame.Height);

                WriteByte(header[9] & 0x07 | 0x07);

                WriteByte(header[10]); //LZW

                // Read/Write image data
                sourceGif.Position = 800;
                var dataLength = sourceGif.ReadByte();
                while (dataLength > 0)
                {
                    var imgData = new byte[dataLength];
                    sourceGif.Read(imgData, 0, dataLength);

                    GifStream.WriteByte(Convert.ToByte(dataLength));
                    await GifStream.WriteAsync(imgData, 0, dataLength);
                    dataLength = sourceGif.ReadByte();
                }

                GifStream.WriteByte(0); // Terminator
            }

        }

        private void WriteColorTable(Stream sourceGif)
        {
            sourceGif.Position = 13; // Locating the image color table
            var colorTable = new byte[768];
            sourceGif.Read(colorTable, 0, colorTable.Length);
            GifStream.Write(colorTable, 0, colorTable.Length);
        }

        private void WriteByte(int value)
        {
            GifStream.WriteByte(Convert.ToByte(value));
        }

        private void WriteShort(int value)
        {
            GifStream.WriteByte(Convert.ToByte(value & 0xff));
            GifStream.WriteByte(Convert.ToByte((value >> 8) & 0xff));
        }

        private async Task WriteString(string value)
        {
            await GifStream.WriteAsync(value.ToArray().Select(c => (byte)c).ToArray(), 0, value.Length);
        }
    }
}