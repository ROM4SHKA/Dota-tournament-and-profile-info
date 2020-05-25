using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KursachV2.Models.Team_Player
{
    public class Team
    {
        [JsonPropertyName("acronym")]
        public string Acronym { get; set; }
        [JsonPropertyName("image_url")]
        public string ImgUrl { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("players")]
        public List<Player> Players { get; set; }
        public Team() { }
    }
}
