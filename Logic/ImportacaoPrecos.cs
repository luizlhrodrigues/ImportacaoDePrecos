using ImportacaoDePrecos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportacaoDePrecos.Logic
{
    public class ImportacaoPrecos
    {
        public void AtualizarFechamento(string acao)
        {
            AlphaVantageApi av = new AlphaVantageApi();

            string result = av.GetTimeSeriesDaily(acao);

            TimeSeriesDaily tsd = JsonConvert.DeserializeObject<TimeSeriesDaily>(result);

            double close = tsd.days["2021-05-18"].close;

        }
    }
}
