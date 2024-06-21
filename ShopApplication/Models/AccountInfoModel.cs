using System.ComponentModel.DataAnnotations;

namespace ShopApplication.Models
{
    // 帳戶資料的模型
    // 並未使用內建的Scaffold來建置，而是對應資料表而手動建置的
    public class AccountInfoModel
    {
        [Key] // 這邊使用'Key'是為了解決'System.InvalidOperationException'
        public Guid AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public int? AccountGender { get; set; } = null;

        [DataType(DataType.Date)]
        public DateTime AccountBirthday { get; set; }
    }
}
