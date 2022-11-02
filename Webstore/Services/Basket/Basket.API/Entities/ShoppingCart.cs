namespace Basket.API.Entities;

public class ShoppingCart
{
    public string Username { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = new();

    public ShoppingCart(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }

    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
}