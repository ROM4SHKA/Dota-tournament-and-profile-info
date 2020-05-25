using System.Text.Json.Serialization;

namespace KursachV2.Models.Team_Player
{
    public class Player
    {
        [JsonPropertyName("birth_year")]
        public int? BirthYear { get; set; }
        [JsonPropertyName("birthday")]
        public string Birth { get; set; }
        [JsonPropertyName("image_url")]
        public string ImgUrl { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("name")]
        public string NickName { get; set; }
        [JsonPropertyName("hometown")]
        public string HomeTown { get; set; }
        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }

        public Player() { }
    }
}
