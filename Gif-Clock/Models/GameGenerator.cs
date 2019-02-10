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
        public List<GifFrame> GenerateImage()
        {
            Image image = new Bitmap(1, 1);
            return new List<GifFrame>() { new GifFrame(image, 0, 0) };
        }
    }
}