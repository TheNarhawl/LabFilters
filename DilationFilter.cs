using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class DilationFilter : MatrixFilter
    {
        public DilationFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            int maxR = 0, maxG = 0, maxB = 0;

            for (int j = -radiusY; j <= radiusY; j++)
            {
                for (int i = -radiusX; i <= radiusX; i++)
                {
                    int neighborX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int neighborY = Clamp(y + j, 0, sourceImage.Height - 1);

                    if (kernel[i + radiusX, j + radiusY] == 1)
                    {
                        Color neighborColor = sourceImage.GetPixel(neighborX, neighborY);
                        maxR = Math.Max(maxR, neighborColor.R);
                        maxG = Math.Max(maxG, neighborColor.G);
                        maxB = Math.Max(maxB, neighborColor.B);
                    }
                }
            }

            return Color.FromArgb(maxR, maxG, maxB);
        }
    }
}

