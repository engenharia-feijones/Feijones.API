using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.API.Model
{
    public class VendaModel
    {

        public int ID { get; set; }
        public int ID_cliente { get; set; }
        public int ID_end { get; set; }
        public decimal total { get; set; }
        public int ID_venda_item { get; set; }


    }
}

