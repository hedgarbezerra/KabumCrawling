using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Repository.Repositories
{
    public class NotificacaoRepository : BaseRepository<NotificacaoProduto>
    {
        public override NotificacaoProduto Inserir(NotificacaoProduto obj)
        {
            obj.DtCadastro = DateTime.Now;
            var objContextual = _context.NotificacaoProdutos.Add(obj);

            return objContextual;
        }
    }
}
