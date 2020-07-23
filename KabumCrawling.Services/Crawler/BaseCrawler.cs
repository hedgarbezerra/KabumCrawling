using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Crawler
{
    public abstract class BaseCrawler
    {
        protected string CorrigeQueryString(string termoBusca = "")
        {
            if (String.IsNullOrEmpty(termoBusca))
                throw new Exception("Houve um erro ao converter o termo de busca, por favor, preenchá-o.");

            return string.Join("+", termoBusca.Replace("  ", " ").Split(' '));

        }
        public string ExtrairQueryString(List<string> removerItens = null)
        {
            removerItens = removerItens ?? new List<string>();

            return string.Join("+", this.GetType()
                                .GetProperties()
                               .Where(x => x.CanRead)
                               .Where(p => p.GetValue(this, null) != null && !string.IsNullOrEmpty(p.GetValue(this, null).ToString())
                                    && p.GetValue(this, null).ToString() != "0" && !removerItens.Exists(z => z == p.Name))
                               .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(this).ToString())}"));
        }
        protected RetornoRequesicao FazerRequest(string endereco, MetodoRequisicao metodo = MetodoRequisicao.GET, List<KeyValuePair<string, string>> formulario = null)
        {
            try
            {
                HttpResponseMessage retornoHttp = null;
                formulario = formulario ?? new List<KeyValuePair<string, string>>();
                FormUrlEncodedContent formUrlEncoded = new FormUrlEncodedContent(formulario);

                using (HttpClient client = new HttpClient { BaseAddress = new Uri(endereco) })
                {
                    if (metodo == MetodoRequisicao.GET)
                    {
                        retornoHttp = client.GetAsync(new Uri(endereco)).Result;
                    }
                    else if(metodo == MetodoRequisicao.POST)
                    {
                        retornoHttp = client.PostAsync(new Uri(endereco), formUrlEncoded).Result;
                    }
                }

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(retornoHttp.Content.ReadAsStringAsync().Result);

                return new RetornoRequesicao
                {
                    HtmlRetorno = htmlDocument,
                    HttpRetorno = retornoHttp
                };
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected enum MetodoRequisicao
        {
            GET, POST, PUT, DELETE
        }
        protected class RetornoRequesicao
        {
            public HtmlDocument HtmlRetorno { get; set; }
            public HttpResponseMessage HttpRetorno { get; set; }
        }
    }
}
 
