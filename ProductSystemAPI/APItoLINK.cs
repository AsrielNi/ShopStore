using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProductSystemAPI.Data;

namespace ProductSystemAPI
{
    public class JsonSetting
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
    }
    public class APItoLINK
    {
        // 整個方案的絕對路徑
        public static readonly string _pathForProject = Directory.GetParent(System.Environment.CurrentDirectory).ToString();
        // WebAPI的名稱
        public static readonly string _apiName = "ProductSystemAPI";
        // WebAPI專案的絕對路徑
        public static readonly string _pathForAPI = _pathForProject + $"\\{_apiName}";
        // WebAPI專案的資源路徑
        public static readonly string _pathForAPIWebRootPath = _pathForAPI + "\\wwwroot";
        // 用於取代'dot'的正規表示法，如果使用string.Replace()的話，會取代所有的'dot'
        public static readonly Regex _connRegex = new Regex(@"\.");

        // 該方法屬於靜態方法，嘗試將該WebAPI的'Main'方法中，抽出必要的原件並作為其他'MVC'模型可連接之方法。
        // 備註：重複的AddControllers和AddEndpointsApiExplorer會不會造成其他影響，目前未知，需要深入了解。
        public static void AttachAPI(WebApplicationBuilder builder)
        {
            // 開啟該API的appsettings.json
            string settingFile = _pathForAPI + "\\appsettings.json";
            string jsonString = File.ReadAllText(settingFile);

            // 對appsettings.json去序列化
            JsonSetting? setting = JsonSerializer.Deserialize<JsonSetting>(jsonString);

            // 取出對應的連接字串
            string? relativeConn = setting.ConnectionStrings["ProductAPIDataBase"];

            // 由於連接字串是使用相對路徑，這裡更改成絕對路徑。
            string connectString = _connRegex.Replace(relativeConn, _pathForAPI, 1);
            
            builder.Services.AddDbContext<ProductContext>(options =>
                options.UseSqlite(connectString));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
        }

        // 該方法屬於靜態方法，嘗試將專案的靜態資源連接到'app'上。
        public static void AttachSource(WebApplication app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(_pathForAPIWebRootPath),
                RequestPath = $"/{_apiName}"
            });
        }
    }
}
