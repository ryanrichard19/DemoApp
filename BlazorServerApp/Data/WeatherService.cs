using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorServerApp.Data
{
    public class WeatherService
    {
        private readonly HttpClient http;
        private readonly TokenProvider tokenProvider;

        public WeatherService(IHttpClientFactory clientFactory,
            TokenProvider tokenProvider)
        {
            http = clientFactory.CreateClient();
            this.tokenProvider = tokenProvider;

        }
        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var token = tokenProvider.AccessToken;
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:7179/WeatherForecast");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
        }
 
};

       
    
}