using System.ComponentModel.DataAnnotations;
using ShopApplication.Areas.LogInSystem.Mapping;

namespace ShopApplication.Areas.LogInSystem.Models
{
    // 對應'RegistrantsModel'的DTO資料模型
    // 主要去掉'AccountID'屬性
    public class RegistrantsModelDTO
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public RegistrantsModelDTO() { }
        public RegistrantsModelDTO(RegistrantsModel model)
        {
            Name = model.Name;
            Password = model.Password;
            Gender = model.Gender;
            Birthday = model.Birthday;
        }
    }
}
