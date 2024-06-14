using System.ComponentModel.DataAnnotations;

namespace ShopApplication.Models
{
    public class CustomerInfo
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerGender { get; set; } = null;
        
        public DateTime CustomerBirthday { get; set; }
    }
}
