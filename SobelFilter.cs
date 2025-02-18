using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class SobelFilter : MatrixFilter
    {
        private float[,] Gx;
        private float[,] Gy;

        private float[,] createGxKernel()
        {
            return new float[,]
            {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
            };
        }

        private float[,] createGyKernel()
        {
            return new float[,]
            {
            { -1, -2, -1 },
            {  0,  0,  0 },
            {  1,  2,  1 }
            };
        }

        public SobelFilter()
        {
            Gx = createGxKernel();
            Gy = createGyKernel();
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = Gx.GetLength(0) / 2;
            int radiusY = Gx.GetLength(1) / 2;
            float resultRx = 0, resultGx = 0, resultBx = 0;
            float resultRy = 0, resultGy = 0, resultBy = 0;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    int kernelX = k + radiusX;
                    int kernelY = l + radiusY;

                    resultRx += neighborColor.R * Gx[kernelX, kernelY];
                    resultGx += neighborColor.G * Gx[kernelX, kernelY];
                    resultBx += neighborColor.B * Gx[kernelX, kernelY];

                    resultRy += neighborColor.R * Gy[kernelX, kernelY];
                    resultGy += neighborColor.G * Gy[kernelX, kernelY];
                    resultBy += neighborColor.B * Gy[kernelX, kernelY];
                }
            }

            int resultR = Clamp((int)Math.Sqrt(resultRx * resultRx + resultRy * resultRy), 0, 255);
            int resultG = Clamp((int)Math.Sqrt(resultGx * resultGx + resultGy * resultGy), 0, 255);
            int resultB = Clamp((int)Math.Sqrt(resultBx * resultBx + resultBy * resultBy), 0, 255);

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
}
