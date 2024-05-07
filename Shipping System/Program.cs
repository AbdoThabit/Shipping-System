using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NToastNotify;
using Shipping_System.BL.Helper;
using Shipping_System.BL.hub;
using Shipping_System.BL.Repositories.AccountRepository;
using Shipping_System.BL.Repositories.BranchRepository;
using Shipping_System.BL.Repositories.CityRepository;
using Shipping_System.BL.Repositories.EmployeeRepository;
using Shipping_System.BL.Repositories.GovernateRepository;
using Shipping_System.BL.Repositories.OrderRepo;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Shipping_System.BL.Repositories.RolesRepository;
using Shipping_System.BL.Repositories.ShippingSettingRepository;
using Shipping_System.BL.Repositories.TraderRepository;
using Shipping_System.BL.Repositories.VillageSettingsRepository;
using Shipping_System.BL.Repositories.WeightSettingsRepository;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;

namespace Shipping_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContextPool<Context>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSignalR();
            builder.Services.AddDbContextPool<Context>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
              options =>
              {
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequireDigit = false;
                  options.Password.RequireLowercase = false;
                  options.Password.RequireUppercase = false;
                  options.Password.RequiredLength = 4;
                  options.User.RequireUniqueEmail = true;

              }
              ).AddEntityFrameworkStores<Context>().AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            builder.Services.AddSession();
            builder.Services.AddScoped< IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<ITraderRepo, TraderRepo>();
            builder.Services.AddScoped<IRepresentativeRepo, RepresentativeRepo>();
            builder.Services.AddScoped<IAccountRepo, AccountRepo>();
            builder.Services.AddScoped<IGovernateRepo, GovernateRepo>();
            builder.Services.AddScoped<ICityRepo,CityRepo>();
            builder.Services.AddScoped<IBranchRepo, BranchRepo>();
            builder.Services.AddScoped<IShippingSettingRepo, ShippingSettingRepo>();
            builder.Services.AddScoped<IWeightSettingsRepo, WeightSettingsRepo>();
            builder.Services.AddScoped<IVillageSettingRepoe, VillageSettingRepoe>();
            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            builder.Services.AddScoped<IAccountRepo, AccountRepo>();
            builder.Services.AddScoped<IRolesRepo, RolesRepo>();
            builder.Services.AddScoped<IMailHelper, MailHelper>();
            builder.Services.AddScoped<PermissionManger, PermissionManger>();




            builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PreventDuplicates = true,
                CloseButton = true,
                PositionClass = ToastPositions.TopCenter,

            });
         builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));



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
                pattern: "{controller=Account}/{action=Login}/{id?}");
            app.MapHub<NotifiactionHub>("/NotifiactionHub");
            app.Run();
        }
    }
}
