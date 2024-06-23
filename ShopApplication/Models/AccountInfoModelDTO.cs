using ShopApplication.DIYtype;
using System.ComponentModel.DataAnnotations;

namespace ShopApplication.Models
{
    public class AccountInfoModelDTO
    {
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public Gender AccountGender { get; set; }

        [DataType(DataType.Date)]
        public DateTime AccountBirthday { get; set; }
    }
}
