namespace LogInAPI.Library
{
    public class Generator
    {
        public static readonly string _randomCharCluster = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string BuildRandomString(int stringLength)
        {
            var random = new Random();
            char[] chars = new char[stringLength];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = _randomCharCluster[random.Next(stringLength)];
            }
            return new string(chars);
        }
    }
}
