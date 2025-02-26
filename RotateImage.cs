using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class RotateImage : Filters
    {
        private double angle;

        public RotateImage(double angle)
        {
            this.angle = angle;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return Color.Black;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            double radians = angle * Math.PI / 180.0;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            int w = sourceImage.Width;
            int h = sourceImage.Height;

            int newW = (int)(Math.Abs(w * cos) + Math.Abs(h * sin));
            int newH = (int)(Math.Abs(w * sin) + Math.Abs(h * cos));

            Bitmap resultImage = new Bitmap(newW, newH);
            int x0 = w / 2, y0 = h / 2;
            int newX0 = newW / 2, newY0 = newH / 2;

            for (int x = 0; x < newW; x++)
            {
                worker.ReportProgress((int)(((float)x / newW) * 100));
                if (worker.CancellationPending)
                    return null;

                for (int y = 0; y < newH; y++)
                {
                    int srcX = (int)Math.Round((x - newX0) * cos + (y - newY0) * sin + x0);
                    int srcY = (int)Math.Round(-(x - newX0) * sin + (y - newY0) * cos + y0);

                    if (srcX >= 0 && srcX < w && srcY >= 0 && srcY < h)
                    {
                        resultImage.SetPixel(x, y, sourceImage.GetPixel(srcX, srcY));
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, Color.White);
                    }
                }
            }

            return resultImage;
        }
    }
}
