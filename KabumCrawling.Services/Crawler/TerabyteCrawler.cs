using HtmlAgilityPack;
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
        private string _baseUrl = "https://www.terabyteshop.com.br/";
        private string _baseListaUrl = "busca?str=";
        public List<Produto> GetGTX1660List()
        {
            string url = "https://www.terabyteshop.com.br/busca?str=GTX+1660";

            RetornoRequesicao requestTera = FazerRequest(url, MetodoRequisicao.GET);

            var listaProdutos = requestTera.HtmlRetorno.GetElementbyId("prodarea").SelectNodes("div[@class='pbox col-xs-12 col-sm-6 col-md-3']/div[@class='commerce_columns_item_inner']").ToList();

            List<Produto> ListaGTX = new List<Produto>();
            ListaGTX.AddRange(HtmlNodeToProductList(listaProdutos));

            return ListaGTX;
        }
        public List<Produto> PesquisarProduto(ProdutoPesquisa produtoPesquisa)
        {
            string url = $"{_baseUrl}{_baseListaUrl}{CorrigeQueryString(produtoPesquisa.produto)}";

            RetornoRequesicao requestTera = FazerRequest(url, MetodoRequisicao.GET);
            List<Produto> produtos = new List<Produto>();

            var listaProdutos = requestTera.HtmlRetorno.GetElementbyId("prodarea").SelectNodes("div[@class='pbox col-xs-12 col-sm-6 col-md-3']/div[@class='commerce_columns_item_inner']").ToList();

            produtos.AddRange(HtmlNodeToProductList(listaProdutos));

            return produtos;
        }

        protected override List<Produto> HtmlNodeToProductList(List<HtmlNode> htmlNodes)
        {
            List<Produto> listaProdutos = new List<Produto>();

            if (htmlNodes.Count > 0)
            {
                foreach (var node in htmlNodes)
                {
                    var preco = node.SelectNodes("div[@class='commerce_columns_item_info']/div/div[@class='prod-new-price']/span");
                    if (preco == null)
                        break;

                    listaProdutos.Add(new Produto
                    {
                        Nome = node.SelectNodes("div[@class='commerce_columns_item_caption']/a[@class='prod-name']").FirstOrDefault().InnerText.Trim(),
                        Preco = TratarReal(preco.FirstOrDefault().InnerText.Replace("R$ ", "").Trim()),
                        UrlImage = node.SelectNodes("div[@class='commerce_columns_item_image text-center']/a[@class='commerce_columns_item_image']/img").FirstOrDefault().Attributes["src"].Value,
                        UrlProduto = node.SelectNodes("div[@class='commerce_columns_item_image text-center']/a[@class='commerce_columns_item_image']").FirstOrDefault().Attributes["href"].Value,
                        Loja = _baseUrl
                    });

                }
            }

            return listaProdutos;
        }

    }
}
