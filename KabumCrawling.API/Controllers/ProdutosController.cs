using KabumCrawling.Domain.Models;
using KabumCrawling.Services.Crawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KabumCrawling.API.Controllers
{
    [RoutePrefix("Produtos")]
    public class ProdutosController : ApiController
    {
        [HttpPost]
        [Route("PesquisarListaProdutos")]
        public IHttpActionResult ListaProdutos([FromBody] ProdutoPesquisa pesquisa)
        {
            try
            {
                List<Produto> produtos = new List<Produto>();
                if(produtos.Count() > 0)
                    return Content(HttpStatusCode.Found, produtos);
                else
                    return Content(HttpStatusCode.NotFound, produtos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("PesquisarProduto")]
        public IHttpActionResult ProdutoUnico([FromBody] ProdutoPesquisa pesquisa)
        {
            try
            {
                ProdutoDetalhado produtos = new ProdutoDetalhado();
                if (produtos != null)
                    return Content(HttpStatusCode.Found, produtos);
                else
                    return Content(HttpStatusCode.NotFound, produtos);
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
                KabumCrawler crawler = new KabumCrawler();

                List<Produto> produtos = new List<Produto>();
                if (produtos.Count() > 0)
                    return Content(HttpStatusCode.Found, produtos);
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
