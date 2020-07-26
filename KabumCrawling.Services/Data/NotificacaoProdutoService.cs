using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Context;
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
        private DestinarioRepository _destinarioRepo;
        private NotificacaoRepository _repo;
        public NotificacaoProdutoService()
        {
            ContextoDados context = new ContextoDados();
            this._destinarioRepo = new DestinarioRepository(context);
            _repo = new NotificacaoRepository(context);
        }
        public NotificacaoProduto CadastrarNotificacao(string emailDestinario, NotificacaoProduto notificacao)
        {

            var destinario = _destinarioRepo.Listar().ToList().Where(x=>x.Email.Contains(emailDestinario)).FirstOrDefault();
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
