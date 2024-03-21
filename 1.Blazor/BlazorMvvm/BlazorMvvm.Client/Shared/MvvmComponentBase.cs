using BlazorMvvm.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BlazorMvvm.Client.Shared
{
    public abstract class MvvmComponentBase<TViewMode> : ComponentBase
        where TViewMode : IViewModelBase
    {
        [Inject]
        [NotNull]
        protected TViewMode ViewModel { get; set; }
        protected override void OnInitialized()
        {
            ViewModel.PropertyChanged += (_, _) => StateHasChanged();
            base.OnInitialized();   
        }
        protected override Task OnInitializedAsync()
        {
            return ViewModel.OnInitializedAsync();
        }
    }
}
