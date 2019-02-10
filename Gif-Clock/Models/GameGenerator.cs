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
        public Image GenerateImage()
        {
            Image image = new Bitmap(1, 1);
            return image;
        }
    }
}