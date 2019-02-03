using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Models
{
    public class Venda
    {
        public int VendaID { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotalItens { get; set; }
        public decimal CashBackTotalVenda { get; set; }
        public virtual ICollection<ItemVenda> Itens { get; set; }        
    }
}
