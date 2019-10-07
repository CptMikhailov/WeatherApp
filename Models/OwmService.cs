using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class OwmService
    {
        private HttpClient Client { get; }
        private string Path { get; }
        public JsonSerializerOptions JsonOptions { get; set; }

        public OwmService(IOptionsMonitor<AppOptions> optionsAccessor, HttpClient client)
        {
            Path = $"{optionsAccessor.CurrentValue.ApiPath}?APPID={optionsAccessor.CurrentValue.ApiKey}";
            client.BaseAddress = new Uri(optionsAccessor.CurrentValue.ApiUrl);
            Client = client;
            JsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };
            JsonOptions.Converters.Add(new DateTimeUnixConverter());
        }

        public async Task<Stream> GetQuery(string q) =>
            await Client.GetStreamAsync($"{Path}&zip={q},us");
    }
}