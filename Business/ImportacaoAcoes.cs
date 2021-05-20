using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ImportacaoDePrecos.Data;
using ImportacaoDePrecos.Models;

namespace ImportacaoDePrecos.Business
{
    public class ImportacaoAcoes
    {
        public void AtualizarFechamento(string acao)
        {
            AlphaVantageApi av = new AlphaVantageApi();
            DataBase db = new DataBase();

            string result = av.GetTimeSeriesDaily(acao);
            var tsd = JsonConvert.DeserializeObject<TimeSeriesDaily>(result);

            DateTime dataInicio = DateTime.Now.AddDays(-7);

            int idAcao = Convert.ToInt32(db.GetAcao(acao).Rows[0].Field<Int64>("IdAcao"));

            foreach (var item in tsd.days.Where(x => Convert.ToDateTime(x.Key) >= dataInicio.Date))
            {
                DateTime data = Convert.ToDateTime(item.Key);

                var dt = db.ConsultaFechamentoAcao(acao, data);


                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0].Field<double>("Valor") != item.Value.close)
                    {
                        var acaoNova = new Acao
                        {
                            IdAcao = idAcao,
                            Fechamentos = new List<FechamentoAcao>() { new FechamentoAcao { Valor = item.Value.close } }
                        };

                        db.UpdateFechamto(acaoNova);
                    }
                }
                else
                {
                    var acaoNova = new Acao
                    {
                        IdAcao = idAcao,
                        Nome = acao,
                        Habilitado = 1,
                        Fechamentos = new List<FechamentoAcao>() { new FechamentoAcao
                        {
                            IdAcao = idAcao,
                            Valor = item.Value.close,
                            Data = data,
                            Ativo = 1
                        }}
                    };

                    db.AddFechamentoAcao(acaoNova);
                }

                //item.Value.close
            }

            var close = tsd.days["2021-05-18"].close;
        }

        //private Acao build(DataTable dt)
        //{
        //    Acao acao = (from item in dt.AsEnumerable()
        //                 select new Acao()
        //                 {
        //                     IdAcao = item.Field<int>("IdAcao"),
        //                     Nome = item.Field<string>("Nome"),
        //                     Habilitado = item.Field<int>("Habilitado"),
        //                     Fechamentos = item.Field<int>("IdAcao"),
        //                     Nome = item.Field<int>("IdAcao"),
        //                 }).FirstOrDefault();
        //}
    }
}
