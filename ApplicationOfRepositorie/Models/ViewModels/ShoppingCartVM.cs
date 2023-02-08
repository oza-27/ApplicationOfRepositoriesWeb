namespace ApplicationOfRepositorie.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> listCarts { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
