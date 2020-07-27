using KabumCrawling.Domain.DTO;
using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Repositories;
using KabumCrawling.Services.Data;
using KabumCrawling.Services.Notification;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KabumCrawling.API.Controllers
{
    [RoutePrefix("api/Notificacoes")]
    public class NotificacaoController : ApiController
    {

        [HttpGet]
        [Route("ListarNotificacoesPorEmail")]
        public IHttpActionResult NotificacoesEmail([FromUri] string email)
        {
            NotificacaoProdutoService service = new NotificacaoProdutoService();
            var listaProdutos = service.ListarNotificacoes(email);
            return Ok(new
            {
                data = listaProdutos,
                message = "Lista de produtos cadastrados para este e-mail encontrada com sucesso."
            });
        }
        [HttpGet]
        [Route("ListarNotificacoes")]
        public IHttpActionResult Notificacoes()
        {
            NotificacaoProdutoService service = new NotificacaoProdutoService();
            var listaProdutos = service.ListarNotificacoes();
            return Ok(new
            {
                data = listaProdutos,
                message = "Lista de produtos encontrada com sucesso."
            });
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

                return Ok(new { 
                    data = objCtx,
                    message = "Notificação de produto cadastrada com sucesso."
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("RemoverNotificacao")]
        public IHttpActionResult RemoverNotificacao([FromBody] DTONotificacaoProduto notificacao)
        {
            try
            {
                NotificacaoProdutoService service = new NotificacaoProdutoService();
                service.RemoverNotificacao(notificacao);
                return Ok(new
                {
                    message = "Notificação de produto descadastrada com sucesso."
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("Email")]
        public IHttpActionResult Email([FromBody] DTONotificacaoProduto notificacaoProduto)
        {
            try
            {
                EmailNotification notification = new EmailNotification();
                NotificacaoProdutoService notifyServ = new NotificacaoProdutoService();
                DestinarioService destinarioService = new DestinarioService();
                CrawlerService crawler = new CrawlerService();

                var produtosParaNotificar = notifyServ.ListarNotificacoes(notificacaoProduto.EmailDestinario);
                var destinario = destinarioService.GetDestinario(notificacaoProduto.EmailDestinario);

                List<Produto> listaProdutos = new List<Produto>();

                foreach (var produtos in produtosParaNotificar)
                {
                    var listaProdutosIteracao = crawler.PesquisarProduto(new DTOProdutoPesquisa { produto = produtos.NomeProduto, valor_produto_min = produtos.ValorMinProduto, valor_produto_max = produtos.ValorMaxProduto }).Take(4);
                    listaProdutosIteracao.ForEach(x =>listaProdutos.Add(x));
                }

                notification.Notificar(listaProdutos, destinario);
                return Ok(new { 
                    message = "E-mail enviado com sucesso."
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
