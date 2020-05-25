using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace KursachV2.Models.Players
{
    public class Profile
    {
        [JsonPropertyName("account_id")]
        public int Accont_id { get; set; }
        [JsonPropertyName("personaname")]
        public string NickName { get; set; }
        [JsonPropertyName("steamid")]
        public string SteamId64 { get; set; }
        [JsonPropertyName("profileurl")]
        public string ProfileURL { get; set; }
        [JsonPropertyName("loccountrycode")]
        public string Location { get; set; }
        [JsonPropertyName("avatarfull")]
        public string ImageURL { get; set; }
        public Profile() { }
    }
}
