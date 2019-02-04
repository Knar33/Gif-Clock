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
        public Stream gifStream { get; set; }
        private List<Color> GlobalColorTable { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }

        public GifEncoder(Stream inputStream, int width, int height, List<Color> globalColorTable)
        {
            //TODO: allow no globalColorTable to be passed in?
            Width = width;
            Height = height;
            gifStream = inputStream;
            if (globalColorTable.Count > 256)
            {
                globalColorTable = globalColorTable.Take(256).ToList();
            }
            GlobalColorTable = globalColorTable;

            GenerateHeader();
        }

        private void GenerateHeader()
        {
            //Header Block
            Task.Run(() => WriteString("GIF89a")).Wait();

            //Logical Screen Descriptor
            WriteShort(Width); //Canvas Width
            WriteShort(Height); //Canvas Height
            //Packed Field
            int packedFieldValue = 16;
            bool globalColorTableIndicator = false;
            if (GlobalColorTable.Count > 0)
            {
                globalColorTableIndicator = true;
                packedFieldValue += 128;
            }
            int globalColorTableSize = 0;
            for (int p = 1; p < 8; p++)
            {
                if (GlobalColorTable.Count > Math.Pow(2, p))
                {
                    globalColorTableSize++;
                }
            }
            packedFieldValue += globalColorTableSize;
            WriteByte(packedFieldValue);
            WriteByte(0); //Background Color Index
            WriteByte(0); //Pixel Aspect Ratio

            //Global Color Table
            if (globalColorTableIndicator)
            {
                for (int i = 0; i < GlobalColorTable.Count; i++)
                {
                    WriteByte(GlobalColorTable[i].R);
                    WriteByte(GlobalColorTable[i].G);
                    WriteByte(GlobalColorTable[i].B);
                }
                int colorsRequired = (int)Math.Pow(2, (globalColorTableSize + 1));
                for (int i = GlobalColorTable.Count; i < colorsRequired; i++)
                {
                    WriteByte(0);
                    WriteByte(0);
                    WriteByte(0);
                }
            }
        }

        public async Task AddFrame(Image frame, int x, int y)
        {
            using (var gifStream = new MemoryStream())
            {
                frame.Save(gifStream, ImageFormat.Gif);
                gifStream.Position = 789; 
                var header = new byte[11];
                gifStream.Read(header, 0, header.Length);
                WriteByte(header[0]);
                WriteShort(x);
                WriteShort(y);
                WriteShort(frame.Height);
                WriteShort(frame.Width);
                WriteByte(header[9] & 0x07 | 0x07);
                WriteByte(header[10]); //LZW

                // Read/Write image data
                gifStream.Position = 800;
                var dataLength = gifStream.ReadByte();
                while (dataLength > 0)
                {
                    var imgData = new byte[dataLength];
                    gifStream.Read(imgData, 0, dataLength);

                    gifStream.WriteByte(Convert.ToByte(dataLength));
                    gifStream.Write(imgData, 0, dataLength);
                    dataLength = gifStream.ReadByte();
                }

                gifStream.WriteByte(0); // Terminator
            }
            
        }

        private void WriteByte(int value)
        {
            gifStream.WriteByte(Convert.ToByte(value));
        }

        private void WriteShort(int value)
        {
            gifStream.WriteByte(Convert.ToByte(value & 0xff));
            gifStream.WriteByte(Convert.ToByte((value >> 8) & 0xff));
        }

        private async Task WriteString(string value)
        {
            await gifStream.WriteAsync(value.ToArray().Select(c => (byte)c).ToArray(), 0, value.Length);
        }
    }
}