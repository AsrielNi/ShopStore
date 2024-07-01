using Microsoft.EntityFrameworkCore;
using ShopApplication.Models;

namespace ShopApplication.Data
{
    // 連接'商家'資料庫用的媒介
    public class ShopContext: DbContext
    {

        // 使用'DbContext'的初始化建構式
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
    }
}
