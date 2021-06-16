using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipliqueV2.API.Model
{
    public class Customer
    {
        public long ID { get; set; }

        public long UserID { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

    }
}
