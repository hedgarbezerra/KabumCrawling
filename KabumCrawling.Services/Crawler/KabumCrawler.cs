using HtmlAgilityPack;
using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Crawler
{
    public class KabumCrawler: BaseCrawler
    {
        private readonly string _baseUrl = "https://www.kabum.com.br/";
        private readonly string _baseListaUrl = "cgi-local/site/listagem/listagem.cgi?string=";
        private readonly List<KeyValuePair<string, string>> _defaultHeaders =  new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(":authority", "https://www.kabum.com.br"),
            new KeyValuePair<string, string>(":method", "GET"),
            new KeyValuePair<string, string>(":scheme", "https"),
            new KeyValuePair<string, string>(":path", $"/"),
            new KeyValuePair<string, string>("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
        };
        public List<Produto> GetGTX1660List()
        {
            List<Cookie> cookiesBase = RequisicaoBase().CookiesRetorno;
            List<KeyValuePair<string, string>> headers = _defaultHeaders;
            headers.Remove(headers.Where(x => x.Key == ":path").FirstOrDefault());
            headers.Add(new KeyValuePair<string, string>(":path", $"{_baseListaUrl}GTX+1660&btnG=&pagina=1&ordem=3&limite=30"));
            string cookiesString = string.Join("; ", cookiesBase.Select(x => $"{x.Name}={x.Value}"));
            headers.Add(new KeyValuePair<string, string>("cookie", cookiesString));
            string url = $"{_baseUrl}{_baseListaUrl}GTX+1660&btnG=&pagina=1&ordem=3&limite=30";

            RetornoRequesicao requestKabum = FazerRequest(url, MetodoRequisicao.GET, cookiesBase, headers: headers);

            return new List<Produto>();
        }

        protected override List<Produto> HtmlNodeToProductList(List<HtmlNode> htmlNodes)
        {
            throw new NotImplementedException();
        }

        private RetornoRequesicao RequisicaoBase()
        {
            return FazerRequest(_baseUrl);
        }
    }
}
