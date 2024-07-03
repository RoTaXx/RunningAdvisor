using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RunningAdvisor.Services

{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApi:ApiKey"];
        }

        public async Task<string> GetCurrentWeatherAsync(string city)
        {
            string url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}&aqi=no";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(data); //parsing json to JObject with Nwtonsoft
                var temperature = json["current"]["temp_c"].ToString();
                var condition = json["current"]["condition"]["text"].ToString();

                return $"Current temperature in {city} is {temperature}°C with {condition}.";
            }
            else
            {
                return "Unable to fetch weather data.";
            }
        }
    }
}
