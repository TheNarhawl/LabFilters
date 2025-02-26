using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class ScharrFilter : MatrixFilter
    {
        private static readonly float[,] kernelX = {
            { -3,  0,  3 },
            { -10, 0, 10 },
            { -3,  0,  3 }
        };

        private static readonly float[,] kernelY = {
            { -3, -10, -3 },
            {  0,   0,  0 },
            {  3,  10,  3 }
        };

        public ScharrFilter() : base() { }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernelX.GetLength(0) / 2;
            int radiusY = kernelX.GetLength(1) / 2;
            float gxR = 0, gxG = 0, gxB = 0;
            float gyR = 0, gyG = 0, gyB = 0;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    gxR += neighborColor.R * kernelX[k + radiusX, l + radiusY];
                    gxG += neighborColor.G * kernelX[k + radiusX, l + radiusY];
                    gxB += neighborColor.B * kernelX[k + radiusX, l + radiusY];

                    gyR += neighborColor.R * kernelY[k + radiusX, l + radiusY];
                    gyG += neighborColor.G * kernelY[k + radiusX, l + radiusY];
                    gyB += neighborColor.B * kernelY[k + radiusX, l + radiusY];
                }
            }

            int r = Clamp((int)Math.Sqrt(gxR * gxR + gyR * gyR), 0, 255);
            int g = Clamp((int)Math.Sqrt(gxG * gxG + gyG * gyG), 0, 255);
            int b = Clamp((int)Math.Sqrt(gxB * gxB + gyB * gyB), 0, 255);

            return Color.FromArgb(r, g, b);
        }
    }
}

