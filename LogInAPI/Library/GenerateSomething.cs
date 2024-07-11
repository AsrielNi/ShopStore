namespace LogInAPI.Library
{
    public class GenerateSomething
    {
        public static readonly string _randomCharCluster = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        // 用於產生隨機16字元的'RegistrantID'
        public static string GenerateRegistrantID(int stringLength)
        {
            var cluser = new char[stringLength];
            var random = new Random();
            for(int i = 0; i < stringLength; i++)
            {
                cluser[i] = _randomCharCluster[random.Next(cluser.Length)];
            }
            return new string(cluser);
        }

        public static string GenerateUpperGuid(Guid guid)
        {
            return guid.ToString().ToUpper();
        }
    }
}
