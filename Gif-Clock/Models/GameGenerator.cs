using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace GifClock
{
    public class GameGenerator : IGenerator
    {
        public Random Rand { get; set; }
        public bool FirstFrame = true;

        public GameGenerator()
        {
            Rand = new Random();
        }

        public List<GifFrame> GenerateImage()
        {
            List<GifFrame> gifFrames = new List<GifFrame>();

            if (FirstFrame)
            {
                gifFrames.Add(GenerateBackground());
                FirstFrame = false;
            }
            else
            {
                gifFrames.Add(GenerateNextFrame());
            }
            return gifFrames;
        }

        public GifFrame GenerateBackground()
        {
            Image image = new Bitmap(500, 500);
            Color backgroundColor = Color.FromArgb(0, 0, 0);
            Graphics drawing = Graphics.FromImage(image);
            drawing.Clear(backgroundColor);
            
            drawing.Save();
            drawing.Dispose();

            return new GifFrame(image, 0, 0);
        }

        public GifFrame GenerateNextFrame()
        {
            Image image = new Bitmap(1, 1);
            Color backgroundColor = Color.FromArgb(255, 255, 255);
            Graphics drawing = Graphics.FromImage(image);
            drawing.Clear(backgroundColor);

            drawing.Save();
            drawing.Dispose();

            return new GifFrame(image, Rand.Next(0, 500), Rand.Next(0, 501));
        }
    }
}