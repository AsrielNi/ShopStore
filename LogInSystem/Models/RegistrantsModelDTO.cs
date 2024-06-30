using System.ComponentModel.DataAnnotations;
using LogInSystem.Mapping;

namespace LogInSystem.Models
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
    }
}
