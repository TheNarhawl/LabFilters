using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class IncreaseBrightness : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);

            int brightnessIncreaseValue = 30;

            int newR = Clamp(sourceColor.R + brightnessIncreaseValue, 0, 255);
            int newG = Clamp(sourceColor.G + brightnessIncreaseValue, 0, 255);
            int newB = Clamp(sourceColor.B + brightnessIncreaseValue, 0, 255);

            Color resultColor = Color.FromArgb(newR, newG, newB);

            return resultColor;
        }
    }
}
