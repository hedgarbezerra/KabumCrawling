using KabumCrawling.Domain.Models;
using KabumCrawling.Repository.Context.Configuration;
using System.Data.Entity;
using System.Linq;

namespace KabumCrawling.Repository.Context
{
    public class ContextoDados : DbContext
    {
        public ContextoDados()
            :base("name=DbNotificar")
        {
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new NotificacaoConfiguration());
            builder.Configurations.Add(new DestinarioConfiguration());
        }
        public virtual DbSet<NotificacaoProduto> NotificacaoProdutos { get; set; }
        public virtual DbSet<Destinario> Destinarios { get; set; }
    }
}
