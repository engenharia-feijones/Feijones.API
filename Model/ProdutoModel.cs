using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.API.Model
{
    public class ProdutoModel
    {

        public int ID { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
         public int qtd { get; set; }
        public string nome { get; set; }
        public string tamanho { get; set; }
    }
}

