using System.ComponentModel.DataAnnotations;

namespace LogInAPI.Models
{
    public class RegistrantDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Gender RegistrantGender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
