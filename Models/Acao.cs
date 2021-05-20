using System;
using System.Collections.Generic;
using System.Text;

namespace ImportacaoDePrecos.Models
{
    public class Acao
    {
        public int? IdAcao { get; set; }
        public string Nome { get; set; }
        public int Habilitado { get; set; }
        public List<FechamentoAcao> Fechamentos { get; set; }
    }
}
