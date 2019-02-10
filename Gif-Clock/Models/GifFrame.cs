using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace GifClock
{
    public class GifFrame
    {
        public Image Frame { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }

        public GifFrame(Image frame, int xOffset, int yOffset)
        {
            Frame = frame;
            XOffset = xOffset;
            YOffset = yOffset;
        }
    }
}