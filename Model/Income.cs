using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipliqueV2.API.Model
{
    public class Income
    {
        public int ID { get; set; }

        public int QuotaID { get; set; }

        public decimal Percentual { get; set; }

        public string Date { get; set; }
    }
}
