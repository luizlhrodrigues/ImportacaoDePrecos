using System;
using System.Data.SQLite;
using ImportacaoDePrecos.Business;
using ImportacaoDePrecos.Data;

namespace ImportacaoDePrecos
{
    class Program
    {
        static void Main(string[] args)
        {
            new DataBase().CreateDataBase();

            ImportacaoAcoes av = new ImportacaoAcoes();

            av.AtualizarFechamento("B3SA3.SAO");
            av.AtualizarFechamento("PETR4.SAO");
        }
    }
}
