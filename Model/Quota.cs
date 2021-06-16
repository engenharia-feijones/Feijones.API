using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipliqueV2.API.Model
{
    public class Quota
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }

        public decimal Value { get; set; }
    }
}
