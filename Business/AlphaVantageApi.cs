using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ImportacaoDePrecos.Business
{
    public class AlphaVantageApi
    {
        private HttpWebRequest _request;
        private HttpWebResponse _response;
        private string _url, _token;

        public AlphaVantageApi()
        {
            _url = ConfigurationManager.AppSettings["AlphaVantageApi_Url"].ToString();
            _token = ConfigurationManager.AppSettings["AlphaVantageApi_Token"].ToString();
        }

        public string GetTimeSeriesDaily(string symbol)
        {
            string function = "TIME_SERIES_DAILY_ADJUSTED";

            _request = (HttpWebRequest)WebRequest.Create(_url + $@"query?function={function}&symbol={symbol}&apikey={_token}");
            _response = (HttpWebResponse)_request.GetResponse();

            StreamReader sr = new StreamReader(_response.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }
    }
}
