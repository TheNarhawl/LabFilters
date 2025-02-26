using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class ErosionFilter : MatrixFilter
    {
        public ErosionFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            int minR = 255, minG = 255, minB = 255;

            for (int j = -radiusY; j <= radiusY; j++)
            {
                for (int i = -radiusX; i <= radiusX; i++)
                {
                    int neighborX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int neighborY = Clamp(y + j, 0, sourceImage.Height - 1);

                    if (kernel[i + radiusX, j + radiusY] == 1.0f)
                    {
                        Color neighborColor = sourceImage.GetPixel(neighborX, neighborY);
                        minR = Math.Min(minR, neighborColor.R);
                        minG = Math.Min(minG, neighborColor.G);
                        minB = Math.Min(minB, neighborColor.B);
                    }
                }
            }

            return Color.FromArgb(minR, minG, minB);
        }
    }
}

