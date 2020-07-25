using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Repositories;
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

        [HttpPost]
        [Route("TesteCadastro")]
        public IHttpActionResult Cadastro([FromBody] NotificacaoProduto notificacaoProduto)
        {
            try
            {
                NotificacaoRepository repo = new NotificacaoRepository();
                var objCtx = repo.Inserir(notificacaoProduto);
                repo.Savechanges();
                return Ok(objCtx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("TesteListar")]
        public IHttpActionResult Listar(string q = "",  int ? qtd_items = null, int? pagina = null)
        {
            try
            {
                NotificacaoRepository repo = new NotificacaoRepository();
                var listaProdutos = repo.Listar(); //repo.Listar(x=> x.NomeDestinario.Contains(q)|| x.NomeProduto.Contains(q), x=> x.DtCadastro, qtd_items, pagina, true);
                return Ok(new { 
                    data = listaProdutos,
                    message = listaProdutos.Count() > 0 ? "Encontramos os seguintes produtos registrados." : "Oops, não encontramos o que procurava."
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}
