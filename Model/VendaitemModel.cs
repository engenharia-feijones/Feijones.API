using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.API.Model
{
    public class VendaItemModel
    {

        public int ID { get; set; }
        public int ID_prod { get; set; }
        public int qtd { get; set; }
        public decimal? preco { get; set; }
        

    }
}

