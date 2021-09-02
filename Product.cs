using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace buildxact_supplies
{
    public class Product
    {
        [Index(0)]        
        public string Id { get; set; }
        [Index(1)]
        public string Description { get; set; }
        [Index(2)]
        [JsonProperty("uom")]
        public string Unit { get; set; }
        [Index(3)]
        [JsonProperty("priceInCents")]
        public double Price { get; set; }
        [Ignore]
        [JsonProperty("materialType")]
        public string MaterialType { get; set; }
    }
}
