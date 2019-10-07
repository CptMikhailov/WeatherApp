using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewComponents
{
    public class WeatherForecast : ViewComponent
    {
        private readonly OwmService _owmService;

        public WeatherForecast(OwmService owmService)
        {
            _owmService = owmService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string zip)
        {
            Models.WeatherForecast model;

            if (string.IsNullOrEmpty(zip))
                return View<Models.WeatherForecast>(null);

            using (var streamTask = _owmService.GetQuery(zip))
                model = await ApiRequest(streamTask);

            return View<Models.WeatherForecast>(model);
        }

        private async ValueTask<Models.WeatherForecast> ApiRequest(Task<Stream> streamTask)
        {
            var model = JsonSerializer.DeserializeAsync<Models.WeatherForecast>(await streamTask, _owmService.JsonOptions);
            return model.Result;
        }
    }
}