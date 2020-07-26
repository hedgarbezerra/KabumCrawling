using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Repository.Repositories
{
    public class DestinarioRepository : BaseRepository<Destinario>
    {
        public override Destinario Inserir(Destinario obj)
        {
            obj.DtCadastro = DateTime.Now;
            var objContextual = _context.Destinarios.Add(obj);

            return objContextual;
        }
    }
}
