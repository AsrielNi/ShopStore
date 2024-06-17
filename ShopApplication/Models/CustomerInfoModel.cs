using System.ComponentModel.DataAnnotations;

namespace ShopApplication.Models
{
    public class CustomerInfoModel
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerGender { get; set; } = null;

        [DataType(DataType.Date)]
        public DateTime CustomerBirthday { get; set; }
    }
}
