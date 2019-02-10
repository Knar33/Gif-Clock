using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace GifClock
{
    public class ClockGenerator : IGenerator
    {
        public TimeZoneInfo timeZoneInfo { get; set; }

        public ClockGenerator(string timeZone)
        {
            try
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            }
            catch (Exception ex)
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("UTC");
            }
        }

        public Image GenerateImage()
        {
            Image image = new Bitmap(62, 18);
            Font font = new Font("Calibri", 12.0f);
            Color textColor = Color.FromArgb(255, 255, 255);
            Color backgroundColor = Color.FromArgb(0, 0, 0);

            Graphics drawing = Graphics.FromImage(image);
            drawing.Clear(backgroundColor);
            Brush textBrush = new SolidBrush(textColor);
            Pen rectPen = new Pen(textBrush);
            drawing.DrawRectangle(rectPen, new Rectangle(0, 0, 61, 17));

            drawing.DrawString(TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo).ToString("HH:mm:ss"), font, textBrush, 0, 0);

            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();

            return image;
        }
    }
}