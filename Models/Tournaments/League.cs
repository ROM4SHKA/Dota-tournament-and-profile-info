using System.Text.Json.Serialization;

namespace KursachV2.Models.Tournaments
{
    public class League
    {
        public League() { }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("image_url")]
        public string ImgUrl { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
