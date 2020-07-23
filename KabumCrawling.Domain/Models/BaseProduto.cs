using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Domain.Models
{
    public abstract class BaseProduto
    {
        public string Nome { get; set; }
        public string UrlImage { get; set; }
        public string UrlProduto { get; set; }
        public decimal Preco { get; set; }

    }
}
