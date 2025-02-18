using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class SepiaFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int intensity = (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
            double k = 1.2;

            int newR = Clamp((int)(intensity * k + 30), 0, 255);
            int newG = Clamp((int)(intensity * k), 0, 255);
            int newB = Clamp((int)(intensity * k - 20), 0, 255);

            Color resultColor = Color.FromArgb(newR, newG, newB);
            return resultColor;
        }
    }
}
