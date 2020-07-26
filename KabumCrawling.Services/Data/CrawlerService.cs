using KabumCrawling.Domain.DTO;
using KabumCrawling.Domain.Models;
using KabumCrawling.Services.Crawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Data
{
    public class CrawlerService
    {
        public List<Produto> PesquisarProduto(DTOProdutoPesquisa pesquisa)
        {
            List<Produto> produtos = new List<Produto>();

            PichauCrawler crawlerPichau = new PichauCrawler();
            TerabyteCrawler terabyteCrawler = new TerabyteCrawler();

            produtos.AddRange(crawlerPichau.PesquisarProduto(pesquisa));
            produtos.AddRange(terabyteCrawler.PesquisarProduto(pesquisa));

            IQueryable<Produto> produtosConsulta = produtos.AsQueryable();

            if (pesquisa.valor_produto_max.HasValue && pesquisa.valor_produto_max > 0)
                produtosConsulta = produtosConsulta.Where(x => x.Preco <= pesquisa.valor_produto_max);

            if (pesquisa.valor_produto_min.HasValue && pesquisa.valor_produto_min > 0)
                produtosConsulta = produtosConsulta.Where(x => x.Preco >= pesquisa.valor_produto_min);

            if (pesquisa.qtd_itens.HasValue)
                produtosConsulta = produtosConsulta.Take(pesquisa.qtd_itens >= 1 ? (int)pesquisa.qtd_itens : produtosConsulta.Count());

            produtos = produtosConsulta.OrderBy(p => p.Preco).ToList();

            return produtos;
        }
        public List<Produto> PesquisarGTX1660(DTOProdutoPesquisa pesquisa)
        {
            PichauCrawler crawlerPichau = new PichauCrawler();
            TerabyteCrawler crawlerTera = new TerabyteCrawler();
            List<Produto> produtos = new List<Produto>();
            produtos.AddRange(crawlerPichau.GetGTX1660List());
            produtos.AddRange(crawlerTera.GetGTX1660List());

            IQueryable<Produto> produtosConsulta = produtos.AsQueryable();

            if (pesquisa.valor_produto_max.HasValue && pesquisa.valor_produto_max > 0)
                produtosConsulta = produtosConsulta.Where(x => x.Preco <= pesquisa.valor_produto_max);

            if (pesquisa.valor_produto_min.HasValue && pesquisa.valor_produto_min > 0)
                produtosConsulta = produtosConsulta.Where(x => x.Preco >= pesquisa.valor_produto_min);

            if (pesquisa.qtd_itens.HasValue)
                produtosConsulta = produtosConsulta.Take(pesquisa.qtd_itens >= 1 ? (int)pesquisa.qtd_itens : produtosConsulta.Count());

            return produtosConsulta.OrderBy(p => p.Preco).ToList();
        }
    }
}
