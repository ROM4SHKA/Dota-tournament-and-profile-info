using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace KursachV2.Models.Players
{
    
    public class CommunityPlayer
    {
        [JsonPropertyName("solo_competitive_rank")]
        public int? SoloCompetetiveRank { get; set; }
        [JsonPropertyName("competitive_rank")]
        public int? CompetetiveRank { get; set; }
        [JsonPropertyName("profile")]
        public Profile profile { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public CommunityPlayer() { }
    }
}
