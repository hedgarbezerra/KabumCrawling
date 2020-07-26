using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Data
{
    public class NotificacaoProdutoService
    {
        private DestinarioService _destinarioService;
        private NotificacaoRepository _repo;
        public NotificacaoProdutoService()
        {
            this._destinarioService = new DestinarioService();
            this._repo = new NotificacaoRepository();
        }
        public NotificacaoProduto CadastrarNotificacao(string emailDestinario, NotificacaoProduto notificacao)
        {
            var destinario = _destinarioService.GetDestinario(emailDestinario);
            notificacao.Destinario = destinario;
            notificacao.IdDestinario = destinario.Id;

            var obj =_repo.Inserir(notificacao);
            _repo.Savechanges();

            return obj;
        }

        public List<NotificacaoProduto> ListarNotificacoes()
        {
            return _repo.Listar().ToList();
        }
        public List<NotificacaoProduto> ListarNotificacoes(string emailDestinario = "")
        {
            return _repo.Listar(x => x.Destinario.Email.Contains(emailDestinario)).ToList();
        }
    }
}
