using KabumCrawling.Domain.DTO;
using KabumCrawling.Domain.Models;
using KabumCrawling.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KabumCrawling.API.Controllers
{
    [RoutePrefix("api/Destinarios")]
    public class DestinariosController : ApiController
    {
        [HttpPost]
        [Route("CadastroDestinario")]
        public IHttpActionResult CadastroDestinario([FromBody] DTODestinario destinario)
        {
            try
            {
                DestinarioService service = new DestinarioService();
                var objCtx = service.CadastrarDestinario(new Destinario
                {
                    Contato = destinario.Contato,
                    Email = destinario.Email,
                    Nome = destinario.Nome
                });
                return Ok(objCtx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("AtualizarDestinario")]
        public IHttpActionResult AtualizarDestinario([FromBody] DTODestinario destinario)
        {
            try
            {
                DestinarioService service = new DestinarioService();

                var objCtx = service.GetDestinarioNoTracking((int)destinario.Id);
                service.AtualizarDestinario(objCtx);
                return Ok(new { 
                message = "Destinário Atualizado com sucesso."
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
