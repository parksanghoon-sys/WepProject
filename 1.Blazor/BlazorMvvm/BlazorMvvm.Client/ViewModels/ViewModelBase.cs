using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazorMvvm.Client.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        [RelayCommand]
        public virtual async Task Loaded()
        {
            await Task.CompletedTask.ConfigureAwait(false);
        }

        public async Task OnInitializedAsync()
        {
            await Loaded().ConfigureAwait(true);
        }
        protected virtual void NotifyStateChanged() => OnPropertyChanged((string?)null);
       
    }
}
