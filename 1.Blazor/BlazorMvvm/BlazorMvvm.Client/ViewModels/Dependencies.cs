namespace BlazorMvvm.Client.ViewModels
{
    public static class Dependencies
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<EditContactViewModel>();
            services.AddTransient<FetchDataViewModel>();
            services.AddTransient<HexEntryViewModel>();
            services.AddTransient<TextEntryViewModel>();
            return services;
        }
    }
}
