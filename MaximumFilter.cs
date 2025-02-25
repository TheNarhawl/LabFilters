using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class MaximumFilter : Filters
    {
        private int radius;

        public MaximumFilter(int radius)
        {
            this.radius = radius;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int maxR = 0;
            int maxG = 0;
            int maxB = 0;

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    if (neighborColor.R > maxR) maxR = neighborColor.R;
                    if (neighborColor.G > maxG) maxG = neighborColor.G;
                    if (neighborColor.B > maxB) maxB = neighborColor.B;
                }
            }

            return Color.FromArgb(maxR, maxG, maxB);
        }
    }
}
