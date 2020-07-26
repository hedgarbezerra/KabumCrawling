using KabumCrawling.Domain.DTO;
using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Repositories;
using KabumCrawling.Services.Crawler;
using KabumCrawling.Services.Data;
using KabumCrawling.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace KabumCrawling.API.Controllers
{
    [RoutePrefix("api/Produtos")]
    public class ProdutosController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pesquisa"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PesquisarPrecosProdutos")]
        public IHttpActionResult ListaProdutos([FromBody] DTOProdutoPesquisa pesquisa)
        {
            try
            {
                CrawlerService crawlerService = new CrawlerService();
                var produtos = crawlerService.PesquisarProduto(pesquisa);

                var retornoApi = new
               {
                   data = produtos,
                   message = produtos.Count > 0 ? $"Encontramos preços do(a) {pesquisa.produto}" : $"Não encontramos preços do(a) {pesquisa.produto}"
                };

                if (produtos.Count() > 0)
                    return Content(HttpStatusCode.Found, retornoApi);
                else
                    return Content(HttpStatusCode.NoContent, retornoApi);
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
                CrawlerService crawlerService = new CrawlerService();
                var produtos = crawlerService.PesquisarGTX1660(pesquisa);

                var retornoApi = new
                {
                    data = produtos,
                    message = produtos.Count > 0 ? "Encontramos preços do(a) GTX 1660" : "Não encontramos preços do(a) 1660"
                };

                if (produtos.Count() > 0)
                    return Content(HttpStatusCode.Found, retornoApi);
                else
                    return Content(HttpStatusCode.NoContent, retornoApi);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
