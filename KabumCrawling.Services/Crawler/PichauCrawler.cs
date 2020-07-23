using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Crawler
{
    public class PichauCrawler : BaseCrawler
    {
        private string BaseUrl = "https://www.pichau.com.br/";
        private string BaseListaUrl = "catalogsearch/result/index/?product_list_order=price&q=";
        public List<Produto> GetGTX1660List()
        {
            string url = "https://www.pichau.com.br/catalogsearch/result/index/?product_list_order=price&q=GTX+1660";

            RetornoRequesicao requestKabum = FazerRequest(url, MetodoRequisicao.GET);

            return new List<Produto>();
        }

    }
}
