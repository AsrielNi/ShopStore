using LogInAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LogInAPI
{
    public class APItoLINK
    {
        // 該方法屬於靜態方法，嘗試將該WebAPI的'Main'方法中，抽出必要的原件並作為其他'MVC'模型可連接之方法。
        // 備註：重複的AddControllers和AddEndpointsApiExplorer會不會造成其他影響，目前未知，需要深入了解。
        public static void AttachAPI(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RegistrantContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("LogInSystemDataBase")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
        }
    }
}
