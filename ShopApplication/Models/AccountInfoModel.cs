using System.ComponentModel.DataAnnotations;
using ShopApplication.DIYtype;

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
        public Gender AccountGender { get; set; }

        [DataType(DataType.Date)]
        public DateTime AccountBirthday { get; set; }

        public AccountInfoModel() { }
        public AccountInfoModel(AccountInfoModelDTO modelDTO)
        {
            this.AccountID = Guid.NewGuid();
            this.AccountName = modelDTO.AccountName;
            this.AccountPassword = modelDTO.AccountPassword;
            this.AccountGender = modelDTO.AccountGender;
            this.AccountBirthday = modelDTO.AccountBirthday;
        }
    }
}
