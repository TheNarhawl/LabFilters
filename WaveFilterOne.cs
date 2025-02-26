using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class WaveFilterOne : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int newX = (int)(x + 20 * Math.Sin(2 * Math.PI * y / 60));
            int newY = y;

            if (newX >= 0 && newX < sourceImage.Width)
            {
                return sourceImage.GetPixel(newX, newY);
            }

            return Color.Black;
        }
    }
}
