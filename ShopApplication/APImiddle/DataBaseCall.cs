using Microsoft.EntityFrameworkCore;

namespace ShopApplication.APImiddle
{
    public class DataBaseCall
    {
        // 測試用的方法，主要測試是否可以不透過前端獲得資料(後端之間的互傳)
        public async Task<string> TestCall(string id)
        {
            string outputString = "";
            using (var context = LogInAPI.APItoLINK.GetContext())
            {
                var result = await context.RegistrantData.FirstOrDefaultAsync(m => m.RegistrantID == id);
                if (result != null)
                {
                    outputString = result.Name;
                }
                else
                {
                    outputString = "not thing";
                }
            }
            return outputString;
        }
    }
}
