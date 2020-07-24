using HtmlAgilityPack;
using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Crawler
{
    public class PichauCrawler : BaseCrawler
    {
        private string _baseUrl = "https://www.pichau.com.br/";
        private string _baseListaUrl = "catalogsearch/result/index/?product_list_order=price&q=";
        public List<Produto> GetGTX1660List()
        {
            List<Produto> listaProdutosDetalhe = new List<Produto>();

            string url = "https://www.pichau.com.br/catalogsearch/result/index/?product_list_order=price&q=GTX+1660";
            RetornoRequesicao requestGTX = FazerRequest(url, MetodoRequisicao.GET);

            var listaProdutos = requestGTX.HtmlRetorno.DocumentNode.SelectNodes("//ol[@class='products list items product-items']").FirstOrDefault().Descendants("li").ToList();

            if (listaProdutos != null)
            {
                listaProdutosDetalhe.AddRange(HtmlNodeToProductList(listaProdutos));
            }

            return listaProdutosDetalhe;
        }
        public List<Produto> PesquisarProduto(ProdutoPesquisa valores)
        {
            string url = $"{_baseUrl}{_baseListaUrl}{CorrigeQueryString(valores.produto)}";
            RetornoRequesicao retornoRequesicao = FazerRequest(url, MetodoRequisicao.GET);

            var listaProdutos = retornoRequesicao.HtmlRetorno.DocumentNode.SelectNodes("//ol[@class='products list items product-items']").FirstOrDefault().Descendants("li").ToList();

            List<Produto> listaProdutosDetalhe = new List<Produto>();
            listaProdutosDetalhe.AddRange(HtmlNodeToProductList(listaProdutos));


            return listaProdutosDetalhe;
        }

        protected override List<Produto> HtmlNodeToProductList(List<HtmlNode> htmlNodes)
        {
            List<Produto> produtosRetorno = new List<Produto>();

            if (htmlNodes.Count > 0)
            {
                foreach (var node in htmlNodes)
                {
                    var preco = node.SelectNodes("div/div/div[@class='price-box price-final_price']/span[@class='price-boleto']/span")
                                ?? node.SelectNodes("div/div/div[@class='price-box price-final_price']/p/span[@class='price-boleto']/span");
                    preco = preco ?? node.SelectNodes("div/div/div[@class='price-box price-final_price']/span[@class='special-price']/span[@class='price-boleto']/span");

                    produtosRetorno.Add(new Produto
                    {
                        Nome = node.SelectNodes("div/div/strong").FirstOrDefault().InnerText.Trim(),
                        Preco = TratarReal(preco.FirstOrDefault().InnerText.Trim()),
                        UrlImage = node.Descendants("img").FirstOrDefault().Attributes["src"].Value,
                        UrlProduto = node.Descendants("a").FirstOrDefault().Attributes["href"].Value,
                        Loja = _baseUrl
                    });
                }
            }

            return produtosRetorno;
        }

        private new decimal TratarReal(string valor)
        {
            try
            {
                valor = valor.Replace("à vista R$", "");

                return Convert.ToDecimal(valor, new CultureInfo("pt-BR"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
