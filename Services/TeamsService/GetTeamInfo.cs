using System;
using System.Collections.Generic;
using KursachV2.Models.Team_Player;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KursachV2.Services.TeamsService
{
    public class GetTeamInfo :ITeam
    {
        public async Task<List<Team>> FullTeamList()
        {
            HttpClient client = new HttpClient();
            string acrList = "Na`Vi,VP,EG,VG,RNG,Liquid,Fnatic,C9,IG,Secret,Gambit,NiP,Allience,Aster,EHOME,Chaos,BC,pain,GeekFam,Adroit,OG";

            string url = $"https://api.pandascore.co/dota2/teams?filter[acronym]={acrList}&token=yfZTFueeWqp6kqJozvgqlu-l0_SqF4ujcQzJ-BYynnSbf1uB918";
            List<Team> t = new List<Team>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage mes = await client.GetAsync(url);
            if (mes.IsSuccessStatusCode)
            {
                var json = await client.GetStreamAsync(url);
                t = await JsonSerializer.DeserializeAsync<List<Team>>(json);
            }
            List<Team> k = new List<Team>();
            foreach (var i in t)
            {
                if (i.Acronym != null)
                    k.Add(i);
            }
            return k;
        }

        public async Task<List<Team>> OneTeamInfo(int id)
        {
            HttpClient client = new HttpClient();
            string url = $"https://api.pandascore.co/dota2/teams?filter[id]={id}&token=yfZTFueeWqp6kqJozvgqlu-l0_SqF4ujcQzJ-BYynnSbf1uB918";
            List <Team> t = new List<Team>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage mes = await client.GetAsync(url);
            if (mes.IsSuccessStatusCode)
            {
                var json = await client.GetStreamAsync(url);
                t = await JsonSerializer.DeserializeAsync<List<Team>>(json);
            }

            return t;
        }
    }
}
