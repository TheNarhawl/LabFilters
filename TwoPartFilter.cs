using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFilters
{
    public class TwoPartFilter
    {
        public Filters LeftFilter { get; set; }
        public Filters RightFilter { get; set; }

        public TwoPartFilter(Filters leftFilter, Filters rightFilter)
        {
            LeftFilter = leftFilter;
            RightFilter = rightFilter;
        }
    }
}
