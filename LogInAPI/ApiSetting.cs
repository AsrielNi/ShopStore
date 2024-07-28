namespace LogInAPI
{
    public class ApiSetting
    {
        // 設置Cookies的有效時間
        public static readonly CookieOptions DefaultCookiesOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1),
        };

        // 設置對應登入依據用的Cookies的名稱
        public static readonly string SessionTag = "LogInSessionID";
    }
}
