using System.ComponentModel.DataAnnotations;

namespace LogInAPI.Models
{
    public class AccountModelDTO
    {
        public string AccountName { get; set; }
        public string Password { get; set; }
        public Gender AccountGender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
