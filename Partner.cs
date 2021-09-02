using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace buildxact_supplies
{
    class Partners
    {
        [JsonProperty("partners")]
        public List<Partner> ListOfPartners { get; set; }
    }

    public class Partner
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("partnerType")]
        public string PartnerType { get; set; }
        [JsonProperty("partnerAddress")]
        public string PartnerAddress { get; set; }
        [JsonProperty("supplies")]
        public List<Product> Supplies { get; set; }
    }
}
