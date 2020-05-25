using System.Text.Json.Serialization;

namespace KursachV2.Models.Tournaments
{
    public class Serie
    {
        [JsonPropertyName("begin_at")]
        public string Begin_at { get; set; }
        [JsonPropertyName("end_at")]
        public string End_at { get; set; }
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("season")]
        public string Season { get; set; }
        [JsonPropertyName("winner_id")]
        public int? WinnerId { get; set; }

    }
}
