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
            int newX = x;
            int newY = (int)(y + 20 * Math.Cos(2 * Math.PI * x / 60));

            if (newY >= 0 && newY < sourceImage.Height)
            {
                return sourceImage.GetPixel(newX, newY);
            }

            return Color.Black;
        }
    }
}
