﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace GifClock
{
    public class Clock : IGenerator
    {
        public TimeZoneInfo timeZoneInfo { get; set; }

        public Clock(string timeZone)
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
            Font font = new Font("Times New Roman", 12.0f);
            Color backgroundColor = Color.FromArgb(255, 255, 255);
            Color textColor = Color.FromArgb(0, 0, 0);

            Graphics drawing = Graphics.FromImage(image);
            drawing.Clear(backgroundColor);
            Brush textBrush = new SolidBrush(textColor);
            
            drawing.DrawString(TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo).ToString("HH:mm:ss"), font, textBrush, 0, 0);

            drawing.Save();
            textBrush.Dispose();
            drawing.Dispose();

            return image;
        }
    }
}