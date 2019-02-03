using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Models
{
    public class ItemVenda
    {
        public int ID { get; set; }        
        public virtual Disco Disco { get; set; }
        public decimal ValorCashBack { get; set; }

        public int DiscoID { get; set; }
        public int VendaID { get; set; }

        [JsonIgnore]
        public virtual Venda Venda { get; set; }
    }
}
