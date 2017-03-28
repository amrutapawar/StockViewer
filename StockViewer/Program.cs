using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;


namespace StockViewer
{
    class Program
    {
        static readonly string FilePath = Path.Combine(Environment.CurrentDirectory, "companylist.csv");
        static readonly string Json_FilePath = Path.Combine(Environment.CurrentDirectory, "stock_json.txt");
        const string Json_Url = "http://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=yhoo&apikey=9360";


        static void Main(string[] args)
        {
         //   Get_Json();
            Read_Json();
            Console.Read();
        }

        static async void Get_Json() {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Json_Url);
            string data = await response.Content.ReadAsStringAsync();
      
               
            data = data.Replace("1. open", "open");
            data = data.Replace("2. high", "high");
            data = data.Replace("3. low", "low");
            data = data.Replace("4. close", "close");
            data = data.Replace("5. volume", "volume");
            File.WriteAllText(Json_FilePath, data);
         
            Console.WriteLine($"file saved to {Json_FilePath}.");


        }

        static void Read_Json() {

         //   var serializer = new JsonSerializer();
            string read = File.ReadAllText(Json_FilePath);
            //using (var reader = new JsonTextReader(read))
            // {
            //    var entries = JsonConvert.DeserializeObject<StockData>(read);// serializer.Deserialize(reader);
            var jo = JObject.Parse(read);
            JToken timeseries = jo.Parent;
            Console.WriteLine(timeseries);
           // }


        }
    }
}
