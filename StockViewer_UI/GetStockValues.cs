using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace StockViewer_UI
{
    public class DailyStockValue {

        public string Date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public UInt64 volume { get; set; }
    }

    class GetStockValues
    {
        readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "companylist.csv");
        public static string Json_FilePath = Path.Combine(Environment.CurrentDirectory, "stock_json.txt");
        public event EventHandler DataReady;
        public string data,company_name;
        private List<DailyStockValue> _stockvalues;

        public IEnumerable<DailyStockValue> StockValues => _stockvalues;

        public GetStockValues() { }

        public GetStockValues(string cname)
        {
            company_name = cname;
        }
     

        public async void Get_Json(string company_name)
        {
            string base_url = "http://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=";
            string Json_Url = String.Concat(base_url + company_name +"&apikey=9360");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Json_Url);
            data = await response.Content.ReadAsStringAsync();


            data = data.Replace("1. open", "open");
            data = data.Replace("2. high", "high");
            data = data.Replace("3. low", "low");
            data = data.Replace("4. close", "close");
            data = data.Replace("5. volume", "volume");
            File.WriteAllText(Json_FilePath, data);
            _stockvalues = Parse_Json();
            //check if data is ready
            DataReady?.Invoke(this,EventArgs.Empty);
                
            Console.WriteLine($"file saved to {Json_FilePath}.");


        }

        public string GetSymbol(string companyname) {
            string[] read = File.ReadAllLines(FilePath);

            Dictionary<string, string> company = new Dictionary<string, string>();
            foreach (var line in read) {
                var array = line.Split(',');
                company.Add(array[0].ToLower(),array[1].ToLower());            
            }

           
           
            foreach (KeyValuePair<string, string> pair in company)
            {
              
             //   if (companyname == mc.Value)
                if((pair.Value).Contains(companyname))
                {
                    string val = pair.Value;
                    return (pair.Key + "," + val);
                }
            }

            return "not found";
        }

        public List<DailyStockValue> Parse_Json()
        {
           
            string read_json = File.ReadAllText(Json_FilePath);
            var jo = JObject.Parse(read_json);
            JToken timeSeries = jo.Last;
            List<DailyStockValue> stocks = new List<DailyStockValue>();
          
            foreach (var ts in timeSeries.Children())
            {
                foreach (var tss in ts.Children())
                {
                    string dsvJson = tss.ToString();
                    int n = dsvJson.IndexOf('{');
                    string values = dsvJson.Substring(n), sDate = dsvJson.Substring(1,10);
                    DailyStockValue dsv = JsonConvert.DeserializeObject<DailyStockValue>(values);
                    // "\"2017-03-21\":"
                    dsv.Date = DateTime.Parse(sDate).ToShortDateString();
                    stocks.Add(dsv);
                }         
            }

            
            return stocks;
        }

     
    }
}
