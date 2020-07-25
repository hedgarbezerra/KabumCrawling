using KabumCrawling.Domain.Models;
using KabumCrawling.Services.Crawler;
using KabumCrawling.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KabumCrawling.API.Controllers
{
    [RoutePrefix("api/Produtos")]
    public class ProdutosController : ApiController
    {
        [HttpPost]
        [Route("PesquisarListaProdutos")]
        public IHttpActionResult ListaProdutos([FromBody] ProdutoPesquisa pesquisa)
        {
            try
            {
                List<Produto> produtos = new List<Produto>();
                PichauCrawler crawlerPichau = new PichauCrawler();
                TerabyteCrawler terabyteCrawler = new TerabyteCrawler();

                produtos.AddRange(crawlerPichau.PesquisarProduto(pesquisa));
                produtos.AddRange(terabyteCrawler.PesquisarProduto(pesquisa));
                if (produtos.Count() > 0)
                {
                    IQueryable<Produto> produtosConsulta = produtos.AsQueryable();

                    if (pesquisa.valor_produto_max.HasValue && pesquisa.valor_produto_max > 0)
                        produtosConsulta = produtosConsulta.Where(x => x.Preco <= pesquisa.valor_produto_max);

                    if (pesquisa.valor_produto_min.HasValue && pesquisa.valor_produto_min > 0)
                        produtosConsulta = produtosConsulta.Where(x => x.Preco >= pesquisa.valor_produto_min);

                    if (pesquisa.qtd_itens.HasValue)
                        produtosConsulta = produtosConsulta.Take(pesquisa.qtd_itens >= 1 ? (int)pesquisa.qtd_itens : produtosConsulta.Count());

                    produtos = produtosConsulta.OrderBy(p => p.Preco).ToList();

                    return Content(HttpStatusCode.Found, produtos);
                }
                else
                    return Content(HttpStatusCode.NoContent, produtos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("PrecosGTX1660")]
        public IHttpActionResult GTX1660([FromBody] ProdutoPesquisa pesquisa)
        {
            try
            {
                PichauCrawler crawlerPichau = new PichauCrawler();
                TerabyteCrawler crawlerTera = new TerabyteCrawler();
                List<Produto> produtos = new List<Produto>();
                produtos.AddRange(crawlerPichau.GetGTX1660List());
                produtos.AddRange(crawlerTera.GetGTX1660List());

                if (produtos.Count() > 0)
                {
                    IQueryable<Produto> produtosConsulta = produtos.AsQueryable();

                    if (pesquisa.valor_produto_max.HasValue && pesquisa.valor_produto_max > 0)
                        produtosConsulta = produtosConsulta.Where(x => x.Preco <= pesquisa.valor_produto_max);

                    if (pesquisa.valor_produto_min.HasValue && pesquisa.valor_produto_min > 0)
                        produtosConsulta = produtosConsulta.Where(x => x.Preco >= pesquisa.valor_produto_min);

                    if (pesquisa.qtd_itens.HasValue)
                        produtosConsulta = produtosConsulta.Take(pesquisa.qtd_itens >= 1 ? (int)pesquisa.qtd_itens : produtosConsulta.Count());

                    produtos = produtosConsulta.OrderBy(p => p.Preco).ToList();

                    return Content(HttpStatusCode.Found, produtos);
                }
                else
                    return Content(HttpStatusCode.NotFound, produtos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
