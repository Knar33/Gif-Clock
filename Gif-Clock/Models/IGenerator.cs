using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace GifClock
{
    public interface IGenerator
    {
        Image GenerateImage();
    }
}