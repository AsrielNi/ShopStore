namespace ShopApplication.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int RemainingQuantity { get; set; }
        public bool IsSell { get; set; }
        public string? PicturePath { get; set; } = null;
    }
}
