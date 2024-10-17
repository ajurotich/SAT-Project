using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAT.UI.MVC.Data;
using SAT.Data.EF.Models;

namespace SAT.UI.MVC;
public class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));
        builder.Services.AddDbContext <SatContext> (options =>options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

		builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
			.AddRoles<IdentityRole>()
			.AddRoleManager<RoleManager<IdentityRole>>()
			.AddEntityFrameworkStores<ApplicationDbContext>();
		builder.Services.AddControllersWithViews();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);// duration a session is stored in memory (default is 20min)
            options.Cookie.HttpOnly = true; // Allows us to set cookie options over nonHTTPS secure connections
            options.Cookie.IsEssential = true; // cannot be declined for session to work
        }
		);

        var app = builder.Build();

		// Configure the HTTP request pipeline.
		if(app.Environment.IsDevelopment()) {
			app.UseMigrationsEndPoint();
		}
		else {
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

        app.UseSession();

        app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
		app.MapRazorPages();

		app.Run();
	}
}
