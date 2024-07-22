using LogInAPI.Data;
using ProductSystemAPI.Data;

namespace ShopApplication.APImiddle
{
    public class DataBaseCall
    {
        public RegistrantContext FromRegistrantContext()
        {
            return LogInAPI.APItoLINK.GetContext();
        }
        public ProductContext FromProductContext()
        {
            return ProductSystemAPI.APItoLINK.GetContext();
        }
    }
}
