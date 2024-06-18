using System.ComponentModel.DataAnnotations;

namespace TestZone.Models
{
    public class UserSessionModel
    {
        [Key]
        public Guid SessionID { get; set; }
        public string Name { get; set; }

        public UserSessionModel() { }
        public UserSessionModel(Guid guid, string name) { SessionID = guid; Name = name;}
    }
}
