using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class MedianFilter : Filters
    {
        private int radius;

        public MedianFilter(int radius)
        {
            this.radius = radius;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            List<int> redValues = new List<int>();
            List<int> greenValues = new List<int>();
            List<int> blueValues = new List<int>();

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    redValues.Add(neighborColor.R);
                    greenValues.Add(neighborColor.G);
                    blueValues.Add(neighborColor.B);
                }
            }

            redValues.Sort();
            greenValues.Sort();
            blueValues.Sort();

            int medianIndex = redValues.Count / 2;
            return Color.FromArgb(
                redValues[medianIndex],
                greenValues[medianIndex],
                blueValues[medianIndex]
            );
        }
    }
}
