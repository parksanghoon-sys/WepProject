using BlazorMvvm.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BlazorMvvm.Client.ViewModels
{
    public partial class EditContactViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ContactInfo _contact = new ContactInfo();
        [ObservableProperty]
        private ObservableCollection<ContactInfo> _contactInfos = new();
        [RelayCommand]
        private void Save()
        {
            ContactInfos.Add(Contact);
            Contact.Validate();
            if (Contact.HasErrors)
                Console.WriteLine("After validating, errors found!");
            else
                Console.WriteLine("Sending contact to server!");
        }

        [RelayCommand]
        protected void ClearForm()
        {
            ContactInfos.Clear();
            Contact = new ContactInfo();
        }
    }
}
