using BlazorMvvm.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Net.WebSockets;

namespace BlazorMvvm.Client.ViewModels
{
    public partial class HexEntryViewModel : RecipientViewModelBase<ConvertAsciiToHexMessage>
    {
        [ObservableProperty]
        private string? _hexText;

        public override void Receive(ConvertAsciiToHexMessage message)
        {
            var charArray = message.asciiToConvert.ToCharArray();
            var hexOputput = string.Empty;

            foreach( var @char in charArray )
            {
                hexOputput += $"{Convert.ToInt32(@char):X}";
            }
            HexText = hexOputput;
        }
        public override Task Loaded()
        {
            IsActive = true;
            return base.Loaded();
        }
        [RelayCommand]
        public virtual void SendToAsciiConverter()
        {
            Messenger.Send(new ConvertHexToAsciiMessage(HexText ?? string.Empty));
        }
    }
}
