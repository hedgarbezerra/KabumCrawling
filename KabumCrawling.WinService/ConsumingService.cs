using KabumCrawling.Domain.DTO;
using KabumCrawling.Domain.Models;
using KabumCrawling.Services.Data;
using KabumCrawling.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.WinService
{
    public static class ConsumingService
    {
        public static void EnviarEmailPrecos()
        {
            EmailNotification notification = new EmailNotification();
            NotificacaoProdutoService notifyServ = new NotificacaoProdutoService();
            DestinarioService destinarioService = new DestinarioService();
            CrawlerService crawler = new CrawlerService();

            var destinarios = destinarioService.ListarDestinarios();

            foreach (var destinario in destinarios)
            {
                List<List<Produto>> listaProdutos = new List<List<Produto>>();

                foreach (var produtos in destinario.Produtos)
                {
                    var listaProdutosIteracao = crawler.PesquisarProduto(new DTOProdutoPesquisa { produto = produtos.NomeProduto, valor_produto_min = produtos.ValorMinProduto, valor_produto_max = produtos.ValorMaxProduto });
                    listaProdutos.Add(listaProdutosIteracao);
                }

                listaProdutos.ForEach(x => notification.Notificar(x, destinario));
            }

        }
    }
}
