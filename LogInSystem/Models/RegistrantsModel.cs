using System.ComponentModel.DataAnnotations;
using LogInSystem.Mapping;

namespace LogInSystem.Models
{
    // 對應'註冊系統'的資料模型
    // 未來考慮加入'Email'屬性
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
