using buildxact_supplies;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SuppliesPriceLister
{
    class Program
    {
        static void Main(string[] args)
        {
            //combinded list
            var list = new List<Product>();
            //Step 1 - Reading CSV file 

            list.AddRange(ReadCSVFile("humphries.csv"));

            //Step 2 - Reading Json file
            var jsonRecords = ReadJsonFile("megacorp.json");

            //Step 3 - Exchanging rate
            ConvertUSDToAUD(jsonRecords);

            //Step 4 - Adding to list for displaying
            list.AddRange(jsonRecords);

            //Step 5 - Adding to list for displaying
            list = list?.OrderByDescending(x => x.Price).ToList();

            //Step 6 - Displaying Records
            foreach (var record in list)
            {
                Console.WriteLine($"{record.Id}, {record.Description}, ${string.Format("{0:#.00}",record.Price)}");
            }
            Console.ReadLine();

        }

        private static double GetExchangeRate()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            return Convert.ToDouble(config["audUsdExchangeRate"]);
        }

        private static List<Product> ReadCSVFile(string csvFile)
        {
            using (var reader = new StreamReader(csvFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Product>();
                return records.ToList() ?? new List<Product>();
            }
        }

        private static List<Product> ReadJsonFile(string jsonFile)
        {
            var list = new List<Product>();
            var myJsonString = File.ReadAllText(jsonFile);
            var jsonList = JsonConvert.DeserializeObject<Partners>(myJsonString);
            foreach (var partner in jsonList?.ListOfPartners)
            {
                list.AddRange(partner?.Supplies);
            }
            return list;
        }

        private static void ConvertUSDToAUD(List<Product> products)
        {
            var exchangeRate = GetExchangeRate();
            foreach (var product in products)
            {
                product.Price = (product.Price * exchangeRate) / 100;
            }
        }
    }
}
