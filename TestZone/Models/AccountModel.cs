using System.ComponentModel.DataAnnotations;

namespace TestZone.Models
{
    public class AccountModel
    {
        [Key]
        public Guid AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public AccountModel() { }
        public AccountModel(AccountModelDTO modelDTO)
        {
            // 這邊自動生成GUID
            this.AccountID = Guid.NewGuid();
            this.AccountName = modelDTO.AccountName;
            this.AccountPassword = modelDTO.AccountPassword;
        }
    }

    // 減少過多的資料張貼
    public class AccountModelDTO
    {
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
    }
}
