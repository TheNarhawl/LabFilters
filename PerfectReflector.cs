using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class PerfectReflector : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return Color.Black;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap resultImage = new Bitmap(width, height);

            int maxR = 0, maxG = 0, maxB = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);
                    if (pixel.R > maxR) maxR = pixel.R;
                    if (pixel.G > maxG) maxG = pixel.G;
                    if (pixel.B > maxB) maxB = pixel.B;
                }
            }

            if (maxR == 0) maxR = 1;
            if (maxG == 0) maxG = 1;
            if (maxB == 0) maxB = 1;

            for (int i = 0; i < width; i++)
            {
                worker.ReportProgress((int)(((float)i / width) * 100));
                if (worker.CancellationPending) return null;

                for (int j = 0; j < height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);

                    int r = Clamp(pixel.R * 255 / maxR, 0, 255);
                    int g = Clamp(pixel.G * 255 / maxG, 0, 255);
                    int b = Clamp(pixel.B * 255 / maxB, 0, 255);

                    resultImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            return resultImage;
        }
    }
}

