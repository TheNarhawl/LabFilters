using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class TopHatFilter : Filters
    {
        private Bitmap originalImage;
        private Bitmap openedImage;

        public TopHatFilter(Bitmap originalImage, Bitmap openedImage)
        {
            this.originalImage = originalImage;
            this.openedImage = openedImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color originalColor = originalImage.GetPixel(x, y);
            Color openedColor = openedImage.GetPixel(x, y);

            int resultR = Clamp(originalColor.R - openedColor.R, 0, 255);
            int resultG = Clamp(originalColor.G - openedColor.G, 0, 255);
            int resultB = Clamp(originalColor.B - openedColor.B, 0, 255);

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
}

