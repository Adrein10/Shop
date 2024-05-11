namespace Shop.Models
{
    public class CustomShop
    {
        public Product? Cproduct { get; set; }
        public IEnumerable<Category>? categorylist { get; set; }
    }
}
