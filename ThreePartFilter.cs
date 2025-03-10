using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    public class ThreePartFilter
    {
        public Filters Filter1 { get; set; }
        public Filters Filter2 { get; set; }
        public Filters Filter3 { get; set; }

        public ThreePartFilter(Filters filter1, Filters filter2, Filters filter3)
        {
            Filter1 = filter1;
            Filter2 = filter2;
            Filter3 = filter3;
        }
    }
}
