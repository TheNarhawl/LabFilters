using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    public class OffsetFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int newX = x - 50;
            if (newX < 0)
                return Color.Black;

            int clampedX = Clamp(newX, 0, sourceImage.Width - 1);
            int clampedY = Clamp(y, 0, sourceImage.Height - 1);
            return sourceImage.GetPixel(clampedX, clampedY);
        }
    }
}
