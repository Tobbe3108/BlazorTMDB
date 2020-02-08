using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorTMDB.Server.Hubs;
using BlazorTMDB.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BlazorTMDB.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TMDBController : ControllerBase, ITMDBController
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<ChatHub> _hubContext;

        public TMDBController(IHttpClientFactory clientFactory, IConfiguration configuration, IHubContext<ChatHub> hubContext)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<Response> Search()
        {
            var request = "https://api.themoviedb.org/3/search/multi?api_key=" + _configuration.GetValue<string>("MySettings:APIKey") + "&language=en-US&query=" + "Iron" + "&page=" + "1" + "&include_adult=false";
            var client = _clientFactory.CreateClient();
            var httpResponse = await client.GetAsync(request);
            var myJObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            Response response = new Response()
            {
                page = myJObject.SelectToken("page").Value<int>(),
                total_results = myJObject.SelectToken("total_results").Value<int>(),
                total_pages = myJObject.SelectToken("total_pages").Value<int>(),
                query = "Iron"
            };

            response.results = ParseTMDBType(myJObject);

            return response;
        }

        public async Task<string> GetImageString(string filePath)
        {
            const string baseUrl = "https://image.tmdb.org/t/p/";
            const string size = "original";
            var client = _clientFactory.CreateClient();
            var bytes = new byte[0];
            try
            {
                bytes = await client.GetByteArrayAsync(baseUrl + size + filePath);
                return "data:image/png;base64," + Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return "data: image / png; base64," + Convert.ToBase64String(BlazorTMDB.Server.Properties.Resources.placeholder_images_image_large);
            }
        }

        public List<Result> ParseTMDBType(JObject myJObject)
        {
            List<Result> results = new List<Result>();
            JArray array = JArray.Parse(myJObject.SelectToken("results").ToString());
            foreach (JObject obj in array.Children<JObject>())
            {
                switch (obj.Value<string>("media_type"))
                {
                    case "movie":
                        Movie movie = new Movie()
                        {
                            id = obj.Value<int>("id"),
                            popularity = obj.Value<double>("popularity"),
                            posterPath = obj.Value<string>("poster_path"),
                            backdropPath = obj.Value<string>("backdrop_path"),
                            voteCount = obj.Value<int>("vote_count"),
                            language = obj.Value<string>("original_language"),
                            title = obj.Value<string>("title"),
                            overview = obj.Value<string>("overview"),
                            releaseDate = obj.Value<DateTime>("release_date"),
                            genreIds = new List<int>()
                        };

                        JArray movieGenreArray = JArray.Parse(obj.SelectToken("genre_ids").ToString());

                        foreach (int item in movieGenreArray)
                        {
                            movie.genreIds.Add(item);
                        }

                        results.Add(movie);
                        break;

                    case "tv":
                        TV tv = new TV()
                        {
                            id = obj.Value<int>("id"),
                            popularity = obj.Value<double>("popularity"),
                            title = obj.Value<string>("name"),
                            voteCount = obj.Value<int>("vote_count"),
                            posterPath = obj.Value<string>("poster_path"),
                            backdropPath = obj.Value<string>("backdrop_path"),
                            overview = obj.Value<string>("overview"),
                            genreIds = new List<int>()
                        };

                        foreach (int item in JArray.Parse(obj.SelectToken("genre_ids").ToString()))
                        {
                            tv.genreIds.Add(item);
                        }

                        results.Add(tv);
                        break;

                    case "person":
                        Person person = new Person()
                        {
                            id = obj.Value<int>("id"),
                            popularity = obj.Value<double>("popularity"),
                            name = obj.Value<string>("name"),
                            job = obj.Value<string>("known_for_department"),
                            posterPath = obj.Value<string>("profile_path"),
                            knownFor = new List<int>()
                        };

                        JArray knownForArray = JArray.Parse(obj.SelectToken("known_for").ToString());

                        foreach (JObject item in knownForArray.Children<JObject>())
                        {
                            person.knownFor.Add(item.Value<int>("id"));
                        }

                        results.Add(person);
                        break;
                }
            }
            return results;
        }
    }
}