using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMvvm.Shared
{
    public partial class ContactInfo : ObservableValidator
    {        
        private string? _name;
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string ? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private string? _email;
        [Required]
        [EmailAddress]
        [MinLength(1)]
        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string? _phoneNumber;
        [Required]
        [Phone]
        public string? PhoneNumber
        {
            get=> _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        public void Validate() => ValidateAllProperties();
    }
}
