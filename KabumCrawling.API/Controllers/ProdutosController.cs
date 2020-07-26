using KabumCrawling.Domain.DTO;
using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Repositories;
using KabumCrawling.Services.Crawler;
using KabumCrawling.Services.Data;
using KabumCrawling.Services.Notification;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace KabumCrawling.API.Controllers
{
    [RoutePrefix("api/Produtos")]
    public class ProdutosController : ApiController
    {
        [HttpPost]
        [Route("PesquisarListaProdutos")]
        public IHttpActionResult ListaProdutos([FromBody] DTOProdutoPesquisa pesquisa)
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
        public IHttpActionResult GTX1660([FromBody] DTOProdutoPesquisa pesquisa)
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
        [Route("CadastroDestinario")]
        public IHttpActionResult CadastroDestinario([FromBody] DTODestinario destinario)
        {
            try
            {
                DestinarioService service = new DestinarioService();
                var objCtx = service.CadastrarDestinario(new Destinario { 
                    Contato = destinario.Contato,
                    Email= destinario.Email,
                    Nome = destinario.Nome
                });
                return Ok(objCtx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Produtos")]
        public IHttpActionResult Produtos([FromUri] string email)
        {
            NotificacaoProdutoService service = new NotificacaoProdutoService();

            return Ok(service.ListarNotificacoes(email));
        }

        [HttpPost]
        [Route("CadastroNotificacao")]
        public IHttpActionResult CadastroNotificacao([FromBody] DTONotificacaoProduto notificacao)
        {
            try
            {
                NotificacaoProdutoService service = new NotificacaoProdutoService();
                var objCtx = service.CadastrarNotificacao(notificacao.EmailDestinario, new NotificacaoProduto
                {
                    NomeProduto = notificacao.NomeProduto,
                    ValorMaxProduto = notificacao.ValorMaxProduto,
                    ValorMinProduto = notificacao.ValorMinProduto                    
                });
                return Ok(objCtx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpPost]
        //[Route("Email")]
        //public IHttpActionResult Email([FromBody] DTONotificacaoProduto notificacaoProduto)
        //{
        //    try
        //    {
        //        NotificacaoRepository repo = new NotificacaoRepository();
        //        EmailNotification notification = new EmailNotification();
        //        var ProdutoUsuario = repo.Listar().FirstOrDefault();
        //        notification.Notificar(new )
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }

        //}
    }
}
