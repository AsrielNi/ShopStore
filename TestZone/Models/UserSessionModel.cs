using System.ComponentModel.DataAnnotations;

namespace TestZone.Models
{
    public class UserSessionModel
    {
        [Key]
        public Guid SessionID { get; set; }
        public string Name { get; set; }
    }
}
