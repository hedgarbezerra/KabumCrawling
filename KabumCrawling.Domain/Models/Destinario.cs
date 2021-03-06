﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Domain.Models
{
    public class Destinario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        public DateTime DtCadastro { get; set; }
        public virtual ICollection<NotificacaoProduto> Produtos { get; set; }
    }
}
