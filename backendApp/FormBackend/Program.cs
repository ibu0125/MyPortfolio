using backendApp.Models; // UserInfo ���f���̖��O��Ԃ��C���|�[�g
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS�̐ݒ��ǉ��i�S�ẴI���W�������j
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// MySQL �f�[�^�x�[�X�̐ݒ�
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))); // ���ϐ��܂��͐ݒ�t�@�C������ڑ���������擾

// �T�[�r�X�̒ǉ�
builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTP���N�G�X�g�̃p�C�v���C���ݒ�
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// CORS���g�p����
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

// ���[�g�}�b�s���O
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
