using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class GlassFilter : Filters
    {
        private Random random = new Random();

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int offsetX = (int)(x + (random.NextDouble() - 0.5) * 10);
            int offsetY = (int)(y + (random.NextDouble() - 0.5) * 10);

            offsetX = Clamp(offsetX, 0, sourceImage.Width - 1);
            offsetY = Clamp(offsetY, 0, sourceImage.Height - 1);

            return sourceImage.GetPixel(offsetX, offsetY);
        }
    }
}
