using LogInAPI.Library;
using System.ComponentModel.DataAnnotations;

namespace LogInAPI.Models
{
    public enum Gender
    {
        Female = 0,
        Male = 1,
        Secret = 2,
    }
    public class AccountModel
    {
        [Key]
        public Guid SessionID { get; set; }
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public Gender AccountGender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public bool IsValid { get; set; }

        public AccountModel() { }
        public AccountModel(AccountModelDTO modelDTO)
        {
            SessionID = Guid.NewGuid();
            AccountID = Generator.BuildRandomString(16);
            AccountName = modelDTO.AccountName;
            Password = modelDTO.Password;
            AccountGender = modelDTO.AccountGender;
            Birthday = modelDTO.Birthday;
            IsValid = true;
        }
    }
}
