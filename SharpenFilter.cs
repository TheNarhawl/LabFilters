using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    class SharpenFilter : MatrixFilter
    {
        private float[,] createSharpenKernel()
        {
            return new float[,]
            {
                {  -1, -1,  -1 },
                { -1,  9, -1 },
                {  -1, -1,  -1 }
            };
        }

        public SharpenFilter()
        {
            kernel = createSharpenKernel();
        }
    }
}
