using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Crawler
{
    public class KabumCrawler: BaseCrawler
    {
        private string BaseUrl = "https://www.kabum.com.br/";
        private string BaseListaUrl = "cgi-local/site/listagem/listagem.cgi?string=";
        public List<Produto> GetGTX1660List()
        {
            string url = "https://www.kabum.com.br/cgi-local/site/listagem/listagem.cgi?string=GTX+1660&btnG=&pagina=1&ordem=3&limite=30";

            RetornoRequesicao requestKabum = FazerRequest(url, MetodoRequisicao.GET);

            return new List<Produto>();
        }
    }
}
