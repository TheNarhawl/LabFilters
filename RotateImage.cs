using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class RotateImage : Filters
    {
        private double angle;
        private int x0, y0;

        public RotateImage(double angle, int x0, int y0)
        {
            this.angle = angle;
            this.x0 = x0;
            this.y0 = y0;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            double radians = angle * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            double newX = (x - x0) * cos - (y - y0) * sin + x0;
            double newY = (x - x0) * sin + (y - y0) * cos + y0;

            int srcX = (int)Math.Round(newX);
            int srcY = (int)Math.Round(newY);

            if (srcX >= 0 && srcX < sourceImage.Width && srcY >= 0 && srcY < sourceImage.Height)
            {
                return sourceImage.GetPixel(srcX, srcY);
            }

            return Color.Black;
        }
    }
}
