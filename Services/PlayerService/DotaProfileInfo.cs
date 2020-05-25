using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using KursachV2.Models.Players;

namespace KursachV2.Services.PlayerService
{
    public class DotaProfileInfo : IPlayer
    {

        public async Task<CommunityPlayer> CPlayerInfo(int? id)
        {
            CommunityPlayer player = new CommunityPlayer();
            HttpClient client = new HttpClient();
            string ID32 = id.ToString();
            string url = $"https://api.opendota.com/api/players/{ID32}?api_key=7B62D942DEE5B85736D444E46E5AA283";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage mes = await client.GetAsync(url);
            if (mes.IsSuccessStatusCode)
            {
                var json = await client.GetStreamAsync(url);
                player = await JsonSerializer.DeserializeAsync<CommunityPlayer>(json);
            }
            var stats = new Stats();
            string url1 = $"https://api.opendota.com/api/players/{ID32}/wl?api_key=7B62D942DEE5B85736D444E46E5AA283";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var message = await client.GetAsync(url1);
            if (message.IsSuccessStatusCode)
            {
                var json = await client.GetStreamAsync(url1);
                stats = await JsonSerializer.DeserializeAsync<Stats>(json);
            }
            player.Wins = stats.Wins;
            player.Loses = stats.Loses;
            return player;
        }

        public async Task<bool> IdIsValid(int? id)
        {
            CommunityPlayer player = new CommunityPlayer();
            HttpClient client = new HttpClient();
            string ID32 = id.ToString();
            string url = $"https://api.opendota.com/api/players/{ID32}?api_key=7B62D942DEE5B85736D444E46E5AA283";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage mes = await client.GetAsync(url);
            if (!mes.IsSuccessStatusCode) { return false; }
            else
            {
                var json = await client.GetStreamAsync(url);
                player = await JsonSerializer.DeserializeAsync<CommunityPlayer>(json);
                if (player.profile == null) { return false; }
                return true;
            }
        }
    }
}
