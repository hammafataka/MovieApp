using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

using Xamarin.Essentials;
using MovieApp.Models;

namespace MovieApp.Services
{
    public class ApiService
    {
        public async  static Task<List<Result>> GetArts(string moviename)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string url = $"https://api.themoviedb.org/3/search/movie?api_key=3332d3120d4e8c55349e7ef463fe36ae&query={moviename}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync(url);
                if (message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    Root root = JsonConvert.DeserializeObject<Root>(json);
                    foreach (Result r in root.results)
                    {
                        r.imageUrl = $"https://image.tmdb.org/t/p/original{r.poster_path}";
                    }
                    return root.results;
                }
            }
            return new List<Result>();
        }
    }
}
