﻿using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Data
{
    public class DestinarioService
    {
        private DestinarioRepository _repo;
        public DestinarioService()
        {
            _repo = new DestinarioRepository();
        }

        public List<Destinario> ListarDestinarios()
        {
            return _repo.Listar().ToList();
        }
        public Destinario GetDestinario(string email = "")
        {
            return _repo.Listar(x => x.Email.Contains(email)).FirstOrDefault();
        }
        public Destinario GetDestinario(int id)
        {
            return _repo.EncontrarPorId(id);
        }
        public Destinario GetDestinarioNoTracking(int id)
        {
            return _repo.GetDestinario(id);
        }
        public Destinario CadastrarDestinario(Destinario destinario)
        {
            var objExistente = _repo.Listar(x => x.Email == destinario.Email).FirstOrDefault();
            if (objExistente != null)
                throw new Exception("Já existe um usuário com este e-mail.");
            var obj = _repo.Inserir(destinario);

            _repo.Savechanges();

            return obj;
        }
        public void AtualizarDestinario(Destinario destinario)
        {
             _repo.Atualizar(destinario);
            _repo.Savechanges();
        }
    }
}
