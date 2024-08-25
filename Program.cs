using LibBooking.Data;
using LibBooking.Models;
using LibBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// 配置SMTP设置
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// 添加EmailService到依赖注入容器
builder.Services.AddTransient<IEmailService, EmailService>();

// 配置数据库
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibBookingContextConnection")));


// 配置Identity服务
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()  // 确保角色管理可用
    .AddEntityFrameworkStores<ApplicationDbContext>();  // 确保使用ApplicationDbContext而不是LibBookingContext

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 配置HTTP请求管道
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // 确保身份验证中间件在授权之前被调用
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Initial}/{id?}");

app.MapRazorPages();


app.Run();


