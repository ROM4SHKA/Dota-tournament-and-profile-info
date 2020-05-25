using System;
using System.Collections.Generic;
using KursachV2.Models.Tournaments;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace KursachV2.Services.TourService
{
    public class GetTourInfo : ITournament
    {

        public async Task<List<Tournament>> GetPastTour()
        {
            HttpClient client = new HttpClient();
            string url = "https://api.pandascore.co/dota2/tournaments/past?&token=yfZTFueeWqp6kqJozvgqlu-l0_SqF4ujcQzJ-BYynnSbf1uB918";
            List<Tournament> t = new List<Tournament>();
            Console.WriteLine("Hello world");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage mes = await client.GetAsync(url);
            if (mes.IsSuccessStatusCode)
            {
                var json = await client.GetStreamAsync(url);
                t = await JsonSerializer.DeserializeAsync<List<Tournament>>(json);
            }
            foreach (var i in t)
            {
                if (i.serie.Begin_at != null)
                    i.serie.Begin_at = i.serie.Begin_at.Remove(10);
                if (i.serie.End_at != null)
                    i.serie.End_at = i.serie.End_at.Remove(10);
            }
            return t;
        }

        public async Task<List<Tournament>> GetRunTour()
        {
            HttpClient client = new HttpClient();
            string url = "https://api.pandascore.co/dota2/tournaments/running?&token=yfZTFueeWqp6kqJozvgqlu-l0_SqF4ujcQzJ-BYynnSbf1uB918";
            List<Tournament> t = new List<Tournament>();
            Console.WriteLine("Hello world");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage mes = await client.GetAsync(url);
            if (mes.IsSuccessStatusCode)
            {
                var json = await client.GetStreamAsync(url);
                t = await JsonSerializer.DeserializeAsync<List<Tournament>>(json);
            }
            foreach (var i in t)
            {
                if (i.serie.Begin_at != null)
                    i.serie.Begin_at = i.serie.Begin_at.Remove(10);
                if (i.serie.End_at != null)
                    i.serie.End_at = i.serie.End_at.Remove(10);
            }
            return t;
        }

        public async Task<List<Tournament>> GetUpTour()
        {
            HttpClient client = new HttpClient();
            string url = "https://api.pandascore.co/dota2/tournaments/upcoming?&token=yfZTFueeWqp6kqJozvgqlu-l0_SqF4ujcQzJ-BYynnSbf1uB918";
            List<Tournament> t = new List<Tournament>();
            Console.WriteLine("Hello world");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage mes = await client.GetAsync(url);
            string s = await client.GetStringAsync(url);
            if (mes.IsSuccessStatusCode)
            {
                var json = await client.GetStreamAsync(url);
                t = await JsonSerializer.DeserializeAsync<List<Tournament>>(json);
            }
            foreach (var i in t)
            {
                if (i.serie.Begin_at != null)
                    i.serie.Begin_at = i.serie.Begin_at.Remove(10);
                if (i.serie.End_at != null)
                    i.serie.End_at = i.serie.End_at.Remove(10);
            }
            return t;
        }
    }
}