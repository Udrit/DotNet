using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using UdritDhakal.Infrastructure.IRepository;
using UdritDhakal.Infrastructure.Repository;
using UdritDhakal.Services.Services;
using UdritDhakal_PMS.Data;
using UdritDhakal_PMS.Models;
using UdritDhakal.Infrastructure;
using UdritDhakal.Infrastructure.Repository.CRUD;
using UdritDhakal.Infrastructure.IRepository.ICrudService;

namespace UdritDhakal_PMS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(connectionString, e => e.MigrationsAssembly("UdritDhakal_PMS")));

            builder.Services.AddTransient(typeof(ICrudService<>), typeof(CrudService<>));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender, EmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedingData.InitializeAsync(services);
            }

            app.Run();
        }
    }
}
