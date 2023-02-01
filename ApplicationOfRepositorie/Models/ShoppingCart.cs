using System.ComponentModel.DataAnnotations;

namespace ApplicationOfRepositorie.Models
{
    public class ShoppingCart
    {

        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Enter the value between 1 and 1000")]
        public int Count { get; set; }
    }
}
