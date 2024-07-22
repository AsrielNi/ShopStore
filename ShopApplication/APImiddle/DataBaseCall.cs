using LogInAPI.Data;
using ProductSystemAPI.Data;

namespace ShopApplication.APImiddle
{
    // 管理WebAPI所使用的類別
    // 目前僅管理Context
    // 未來有可能會將方法改成static
    public class DataBaseCall
    {
        // 引用LogInAPI的RegistrantContext
        public RegistrantContext FromRegistrantContext()
        {
            return LogInAPI.APItoLINK.GetContext();
        }

        // 引用ProductSystemAPI的ProductContext
        public ProductContext FromProductContext()
        {
            return ProductSystemAPI.APItoLINK.GetContext();
        }
    }
}
