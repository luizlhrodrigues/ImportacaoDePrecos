using System;
using System.Collections.Generic;
using System.Text;

namespace ImportacaoDePrecos.Models
{
    public class FechamentoAcao
    {
        public int IdFechamento { get; set; }
        public int IdAcao { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public int Ativo { get; set; }
    }
}
