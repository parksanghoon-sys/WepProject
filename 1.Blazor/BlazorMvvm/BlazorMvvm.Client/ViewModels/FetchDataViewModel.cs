using BlazorMvvm.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace BlazorMvvm.Client.ViewModels
{
    public partial class FetchDataViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient;
        public FetchDataViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [ObservableProperty]
        private ObservableCollection<WeatherForecast> _weatherForecasts = new();
        public override async Task Loaded()
        {
            if(await _httpClient.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast") is { } result)
            {
                _weatherForecasts = new ObservableCollection<WeatherForecast>(result);
            }
        }
    }
}
