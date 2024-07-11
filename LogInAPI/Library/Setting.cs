namespace LogInAPI.Library
{
    public class Setting
    {
        public static readonly CookieOptions DefaultCookiesOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1),
        };

        public static readonly string SessionTag = "RegistrantSessionID";
    }
}
