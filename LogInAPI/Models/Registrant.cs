using System.ComponentModel.DataAnnotations;

namespace LogInAPI.Models
{
    public enum Gender
    {
        Female = 0,
        Male = 1,
        Secret = 2,
    }
    public class Registrant
    {
        public Guid SerialID { get; set; }
        public string RegistrantID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Gender RegistrantGender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public Registrant() { }
        public Registrant(RegistrantDTO modelDTO, string registrantID)
        {
            this.SerialID = Guid.NewGuid();
            this.RegistrantID = registrantID;
            this.Name = modelDTO.Name;
            this.Password = modelDTO.Password;
            this.RegistrantGender = modelDTO.RegistrantGender;
            this.Birthday = modelDTO.Birthday;
        }
    }
}
