using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace KursachV2.Models.Players
{
    public class Stats
    {
        [JsonPropertyName("win")]
        public int Wins { get; set; }
        [JsonPropertyName("lose")]
        public int Loses { get; set; }
        public Stats() { }
    }
}
