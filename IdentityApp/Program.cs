using IdentityApp.Data;
using IdentityApp.Data.Entity;
using IdentityApp.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddDbContext<ProjDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//builder.Services.AddIdentityCore<User>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//})
//.AddRoles<IdentityRole>()
//.AddEntityFrameworkStores<ProjDbContext>()
//.AddUserManager<UserManager<User>>()
//.AddRoleManager<RoleManager<IdentityRole>>()
//.AddDefaultTokenProviders();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ProjDbContext>()
.AddDefaultTokenProviders();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


//builder.Services.AddScoped<SignInManager<User>>();

//register dependency
builder.Services.AddScoped<IAccountService, AccountService>();

//builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
//    .AddCookie(IdentityConstants.ApplicationScheme, options =>
//    {
//        options.LoginPath = "/Account/Login";
//        options.AccessDeniedPath = "/Account/AccessDenied";
//    });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//sedding users with roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DbSeeder.SeedAsync(userManager, roleManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();



app.MapStaticAssets();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



app.UseAuthorization();
app.Run();
