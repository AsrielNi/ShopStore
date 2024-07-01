using System.ComponentModel.DataAnnotations;
using ShopApplication.Areas.LogInSystem.Mapping;

namespace ShopApplication.Areas.LogInSystem.Models
{
    public class RegistrantsModel
    {
        [Key]
        public Guid AccountID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public RegistrantsModel() { }

        public RegistrantsModel(RegistrantsModelDTO modelDTO)
        {
            Guid accountID = Guid.NewGuid();
            Name = modelDTO.Name;
            Password = modelDTO.Password;
            Gender = modelDTO.Gender;
            Birthday = modelDTO.Birthday;
        }
    }
}
