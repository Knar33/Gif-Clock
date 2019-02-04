using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace GifClock
{
    public class Clock
    {
        public static Image GenerateTime()
        {
            Image image = new Bitmap(62, 18);
            Graphics drawing = Graphics.FromImage(image);
            Font font = new Font("Times New Roman", 12.0f);
            Color backgroundColor = Color.FromArgb(255, 255, 255);
            Color textColor = Color.FromArgb(0, 0, 0);

            drawing = Graphics.FromImage(image);
            drawing.Clear(backgroundColor);
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(DateTime.Now.ToString("HH:mm:ss"), font, textBrush, 0, 0);

            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();

            return image;
        }
    }
}