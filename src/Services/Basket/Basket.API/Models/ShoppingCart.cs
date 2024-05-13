namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; } = new();
        public string UserName { get; set; } = default!;
        public decimal TotalPrice => Items
            .Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string username)
        {
            UserName = username;
        }
        public ShoppingCart()
        {

        }
    }
}