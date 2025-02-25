using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter(int size)
        {
            kernel = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                kernel[i, i] = 1.0f;
            }

            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sum += kernel[i, j];
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    kernel[i, j] /= sum;
                }
            }
        }
    }
}
