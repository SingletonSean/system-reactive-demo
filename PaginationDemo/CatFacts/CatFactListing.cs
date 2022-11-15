using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationDemo.CatFacts
{
    public class CatFactListing
    {
        public IEnumerable<CatFact> CatFacts { get; set; }
        public int Total { get; set; }
    }
}
