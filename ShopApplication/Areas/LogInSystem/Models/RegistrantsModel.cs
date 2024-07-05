using System.ComponentModel.DataAnnotations;
using ShopApplication.Areas.LogInSystem.Mapping;

namespace ShopApplication.Areas.LogInSystem.Models
{
    // 帳戶資料的模型
    // 並未使用內建的Scaffold來建置，而是對應資料表而手動建置的
    public class RegistrantsModel
    {
        [Key] // 這邊使用'Key'是為了解決'System.InvalidOperationException'
        public Guid AccountID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public RegistrantsModel() { } // 解決存在帶有參數的建構式的問題

        public RegistrantsModel(RegistrantsModelDTO modelDTO)
        {
            Guid accountID = Guid.NewGuid();
            Name = modelDTO.Name;
            Password = modelDTO.Password;
            Gender = modelDTO.Gender;
            Birthday = modelDTO.Birthday;
        }
        public RegistrantsModel(RegistrantsModelDTO modelDTO, Guid guid)
        {
            AccountID = guid;
            Name = modelDTO.Name;
            Password = modelDTO.Password;
            Gender = modelDTO.Gender;
            Birthday = modelDTO.Birthday;
        }
    }
}
