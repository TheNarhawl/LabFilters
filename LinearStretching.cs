using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class LinearStretching : Filters
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

            int minBrightness = 255;
            int maxBrightness = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);
                    int brightness = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    if (brightness < minBrightness) minBrightness = brightness;
                    if (brightness > maxBrightness) maxBrightness = brightness;
                }
            }

            if (minBrightness == maxBrightness) return sourceImage;

            for (int i = 0; i < width; i++)
            {
                worker.ReportProgress((int)(((float)i / width) * 100));
                if (worker.CancellationPending) return null;

                for (int j = 0; j < height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);

                    int r = Clamp((pixel.R - minBrightness) * 255 / (maxBrightness - minBrightness), 0, 255);
                    int g = Clamp((pixel.G - minBrightness) * 255 / (maxBrightness - minBrightness), 0, 255);
                    int b = Clamp((pixel.B - minBrightness) * 255 / (maxBrightness - minBrightness), 0, 255);

                    resultImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            return resultImage;
        }
    }
}