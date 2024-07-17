namespace ProductSystemAPI.Library
{
    public static class GenerateSomething
    {
        public static readonly string _randomCharCluster = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string GenerateProductID(int stringLength)
        {
            var cluster = new char[stringLength];
            var random = new Random();
            for (int i = 0; i < stringLength; i++)
            {
                cluster[i] = _randomCharCluster[random.Next(stringLength)];
            }
            return new string(cluster);
        }
    }
}
