using System.ComponentModel.DataAnnotations;

namespace TestZone.Models
{
    public class AccountModel
    {
        [Key]
        public Guid AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
    }
}
