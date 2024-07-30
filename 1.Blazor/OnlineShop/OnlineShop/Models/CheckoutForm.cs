using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class CheckoutForm
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "We need the address to deliver to product")]
        public string Address { get; set; } = string.Empty;
    }
}
