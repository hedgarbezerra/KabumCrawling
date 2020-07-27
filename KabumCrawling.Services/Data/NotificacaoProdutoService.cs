using KabumCrawling.Domain.DTO;
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

            var destinario = _destinarioRepo.Listar(x => x.Email.Contains(emailDestinario)).ToList().FirstOrDefault();
            notificacao.Destinario = destinario;
            notificacao.IdDestinario = destinario.Id;

            var obj =_repo.Inserir(notificacao);
            _repo.Savechanges();

            return obj;
        }
        public void RemoverNotificacao(DTONotificacaoProduto notificacao)
        {
            //var destinario = _destinarioRepo.Listar().ToList().Where(x => x.Email.Contains(emailDestinario)).FirstOrDefault();
            NotificacaoProduto notificacaoAchada = notificacao.Id.HasValue ? _repo.EncontrarPorId((int)notificacao.Id) : _repo.Listar(x => x.Destinario.Email == notificacao.EmailDestinario && x.NomeProduto.ToLower().Contains(notificacao.NomeProduto)).FirstOrDefault();
            _repo.Remover(notificacaoAchada);
            _repo.Savechanges();

        }

        public List<NotificacaoProduto> ListarNotificacoes()
        {
            return _repo.Listar().ToList();
        }
        public List<NotificacaoProduto> ListarNotificacoes(string emailDestinario = "")
        {
            return _repo.Listar(x => x.Destinario.Email.Contains(emailDestinario)).ToList();
        }
        public List<NotificacaoProduto> ListarNotificacoesNoTracking(string emailDestinario = "")
        {
            return _repo.ListarNoTracking().Where(x => x.Destinario.Email.Contains(emailDestinario)).ToList();
        }
    }
}
