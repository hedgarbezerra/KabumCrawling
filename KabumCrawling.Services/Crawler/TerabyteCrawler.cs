using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Crawler
{
    public class TerabyteCrawler : BaseCrawler
    {
        private string BaseUrl = "https://www.terabyteshop.com.br/";
        private string BaseListaUrl = "busca?str=";
        public List<Produto> GetGTX1660List()
        {
            string url = "https://www.terabyteshop.com.br/busca?str=GTX+1660";

            RetornoRequesicao requestKabum = FazerRequest(url, MetodoRequisicao.GET);

            return new List<Produto>();
        }
        public List<Produto> ListaProdutos(string produto)
        {
            string termoBusca = "busca?";
           
            RetornoRequesicao requestKabum = FazerRequest(termoBusca, MetodoRequisicao.GET);

            return new List<Produto>();
        }
    }
}
