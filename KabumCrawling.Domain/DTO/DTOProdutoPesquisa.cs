using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Domain.DTO
{
    public class DTOProdutoPesquisa
    {
        public string produto { get; set; }
        public decimal? valor_produto_min { get; set; }
        public decimal? valor_produto_max { get; set; }
        public int? qtd_itens { get; set; }
    }
}
