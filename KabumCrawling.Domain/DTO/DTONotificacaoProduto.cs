using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Domain.DTO
{
    public class DTONotificacaoProduto
    {
        public int? Id { get; set; }
        public string EmailDestinario { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorMinProduto { get; set; }
        public decimal ValorMaxProduto { get; set; }
        public DateTime DtCadastro { get; set; }
    }
}
