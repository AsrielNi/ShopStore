using System.ComponentModel.DataAnnotations;

namespace ShopApplication.Models
{
    // 消費者資料的模型
    // 並未使用內建的Scaffold來建置，而是對應資料表而手動建置的
    public class CustomerInfoModel
    {
        [Key] // 這邊使用'Key'是為了解決'System.InvalidOperationException'
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerGender { get; set; } = null;

        [DataType(DataType.Date)]
        public DateTime CustomerBirthday { get; set; }
    }
}
