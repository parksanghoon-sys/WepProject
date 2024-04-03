using System.ComponentModel;

namespace BlazorMvvm.Client.ViewModels
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Task OnInitializedAsync();
        Task Loaded();
    }
}
