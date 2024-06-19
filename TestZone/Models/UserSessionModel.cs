using System.ComponentModel.DataAnnotations;

namespace TestZone.Models
{
    // 測試'Cookies - Session'用的資料模型
    public class UserSessionModel
    {
        [Key]
        public Guid SessionID { get; set; }
        public string Name { get; set; }

        // 因為新增一個'帶有參數'的建構式，所以必須設置一個'不帶有參數'的建構式
        // DTOmodel也會遇到同樣的問題
        public UserSessionModel() { }
        public UserSessionModel(Guid guid, string name) { SessionID = guid; Name = name;}
    }
}
