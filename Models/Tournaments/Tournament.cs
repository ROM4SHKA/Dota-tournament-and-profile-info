using System.Collections.Generic;
using System.Text.Json.Serialization;
using KursachV2.Models.Team_Player;

namespace KursachV2.Models.Tournaments
{
    public class Tournament
    {
        public Tournament() { }
        [JsonPropertyName("begin_at")]
        public string Begin_at { get; set; }
        [JsonPropertyName("end_at")]
        public string End_at { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("league")]
        public League League { get; set; }
        [JsonPropertyName("prizepool")]
        public string PrizePool { get; set; }
        [JsonPropertyName("teams")]
        public List<Team> Teams { get; set; }
        [JsonPropertyName("winner_id")]
        public int? Winnerid { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("serie")]
        public Serie serie { get; set; }
    }
}
