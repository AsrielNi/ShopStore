using Microsoft.EntityFrameworkCore;
using TestZone.Models;

namespace TestZone.Data
{
    // 測試用資料庫的媒介
    public class TestContext : DbContext
    {
        // 使用'DbContext'的初始化建構式
        public TestContext(DbContextOptions<TestContext> options) : base(options) { }

        // 測試'Session'用的資料表
        public DbSet<UserSessionModel> UserSession { get; set; }
    }
}
