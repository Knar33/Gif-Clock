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

        public GifEncoder(Stream inputStream)
        {
            GifStream = inputStream;
            FirstFrame = true;
        }

        private void GenerateHeader(MemoryStream firstFrameStream)
        {
            firstFrameStream.Position = 0;
            var header = new byte[781];
            firstFrameStream.Read(header, 0, header.Length);
            GifStream.Write(header, 0, header.Length);
        }

        public async Task AddFrame(Image frame, int x, int y)
        {
            using (var sourceImage = new MemoryStream())
            {
                frame.Save(sourceImage, ImageFormat.Gif);
                if (FirstFrame)
                {
                    GenerateHeader(sourceImage);
                    FirstFrame = false;
                }
                sourceImage.Position = 789; //Position of the Image Descriptor
                var header = new byte[11];
                sourceImage.Read(header, 0, header.Length);
                WriteByte(header[0]);
                WriteShort(x);
                WriteShort(y);
                WriteShort(frame.Width);
                WriteShort(frame.Height);

                WriteByte(header[9] & 0x07 | 0x07);

                WriteByte(header[10]); //LZW

                // Read/Write image data
                sourceImage.Position = 800;
                var dataLength = sourceImage.ReadByte();
                while (dataLength > 0)
                {
                    var imgData = new byte[dataLength];
                    sourceImage.Read(imgData, 0, dataLength);

                    GifStream.WriteByte(Convert.ToByte(dataLength));
                    await GifStream.WriteAsync(imgData, 0, dataLength);
                    dataLength = sourceImage.ReadByte();
                }

                GifStream.WriteByte(0); // Terminator
            }

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