using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BPMS_2.Data;
using Microsoft.AspNetCore.Identity;
using BPMS_2.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BPMS_2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BPMS_2Context") ?? throw new InvalidOperationException("Connection string 'BPMS_2Context' not found.")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<BPMS_2Context>()
        .AddDefaultTokenProviders()
        .AddPasswordValidator<CustomPasswordValidator<IdentityUser>>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.Configure<PasswordHasherOptions>(option =>
{
    option.IterationCount = 100000;
});


builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    //options.Cookie.Name = "session";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    //options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;


    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.ReturnUrlParameter = "/Account/Login";
    options.SessionStore = new MemoryCacheStore();

});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
