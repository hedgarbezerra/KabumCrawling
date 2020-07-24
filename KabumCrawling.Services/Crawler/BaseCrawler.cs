using HtmlAgilityPack;
using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Crawler
{
    public abstract class BaseCrawler
    {
        private readonly string _userAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36";
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
        protected virtual decimal TratarReal(string valor)
        {
            try
            {
                return Convert.ToDecimal(valor,  new CultureInfo("pt-BR"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected abstract List<Produto> HtmlNodeToProductList(List<HtmlNode> htmlNodes);
        protected virtual RetornoRequesicao FazerRequest(string endereco, MetodoRequisicao metodo = MetodoRequisicao.GET, List<Cookie> cookies = null, List<KeyValuePair<string, string>> formulario = null, List<KeyValuePair<string, string>> headers = null)
        {
            try
            {
                HttpResponseMessage retornoHttp = null;
                CookieContainer cookieContainer = new CookieContainer();

                if (cookies != null && cookies.Count > 0)
                    cookies.ForEach(cookie => cookieContainer.Add(cookie));

                HttpClientHandler httpHandler = new HttpClientHandler { CookieContainer = cookieContainer };
                
                using (HttpClient client = new HttpClient(httpHandler) { BaseAddress = new Uri(endereco)})
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);

                    if (headers != null && headers.Count > 0)
                        headers.ForEach(header => client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value));

                    if (metodo == MetodoRequisicao.GET)
                    {
                        retornoHttp = client.GetAsync(new Uri(endereco)).Result;
                    }
                    else if (metodo == MetodoRequisicao.POST)
                    {
                        formulario = formulario ?? new List<KeyValuePair<string, string>>();
                        FormUrlEncodedContent formUrlEncoded = new FormUrlEncodedContent(formulario);

                        retornoHttp = client.PostAsync(new Uri(endereco), formUrlEncoded).Result;
                    }
                }         

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(retornoHttp.Content.ReadAsStringAsync().Result);

                return new RetornoRequesicao
                {
                    HtmlRetorno = htmlDocument,
                    HttpRetorno = retornoHttp,
                    CookiesRetorno = httpHandler.CookieContainer.GetCookies(new Uri(endereco)).Cast<Cookie>().ToList()
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
            public List<Cookie> CookiesRetorno { get; set; }
        }
    }
}
 
