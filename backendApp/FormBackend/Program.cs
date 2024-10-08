using backendApp.Models; // UserInfo モデルの名前空間をインポート
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORSの設定を追加（全てのオリジンを許可）
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// MySQL データベースの設定
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))); // 環境変数または設定ファイルから接続文字列を取得

// サービスの追加
builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTPリクエストのパイプライン設定
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// CORSを使用する
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

// ルートマッピング
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
