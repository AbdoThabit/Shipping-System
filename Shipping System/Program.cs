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
            #region policies
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("addEmployeePloicy", policy => policy.RequireClaim("اضافة موظف"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editEmployeePloicy", policy => policy.RequireClaim("تعديل موظف"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteEmployeePloicy", policy => policy.RequireClaim("حذف موظف"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewEmployeePloicy", policy => policy.RequireClaim("عرض الموظفين"));

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addTraderPloicy", policy => policy.RequireClaim("اضافة تاجر"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editTraderPloicy", policy => policy.RequireClaim("تعديل تاجر"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteTraderPloicy", policy => policy.RequireClaim("حذف تاجر"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewTraderPloicy", policy => policy.RequireClaim("عرض التجار"));

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addRepresentivePloicy", policy => policy.RequireClaim("اضافة مندوب"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editRepresentivePloicy", policy => policy.RequireClaim("تعديل مندوب"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteRepresentivePloicy", policy => policy.RequireClaim("حذف مندوب"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewRepresentivePloicy", policy => policy.RequireClaim("عرض المناديب"));

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addGovernatePloicy", policy => policy.RequireClaim("اضافة محافظة"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editGovernatePloicy", policy => policy.RequireClaim("تعديل محافظة"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteGovernatePloicy", policy => policy.RequireClaim("حذف محافظة"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewGovernatePloicy", policy => policy.RequireClaim("عرض المحافظات"));

            

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addCityPloicy", policy => policy.RequireClaim("اضافة مدينة"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editCityPloicy", policy => policy.RequireClaim("تعديل مدينة"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteCityPloicy", policy => policy.RequireClaim("حذف مدينة"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewCityPloicy", policy => policy.RequireClaim("عرض المدن"));

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addBranchPloicy", policy => policy.RequireClaim("اضافة فرع"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editBranchPloicy", policy => policy.RequireClaim("تعديل فرع"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteBranchPloicy", policy => policy.RequireClaim("حذف فرع"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewBranchPloicy", policy => policy.RequireClaim("عرض الافرع"));

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addRolePloicy", policy => policy.RequireClaim("اضافة صلاحية"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editRolePloicy", policy => policy.RequireClaim("تعديل صلاحية"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteRolePloicy", policy => policy.RequireClaim("حذف صلاحية"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewRolePloicy", policy => policy.RequireClaim("عرض الصلاحيات"));

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addOrderPloicy", policy => policy.RequireClaim("اضافة طلب"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editOrderPloicy", policy => policy.RequireClaim("تعديل طلب"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editOrderStatusPloicy", policy => policy.RequireClaim("تعديل حالة طلب"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteOrderPloicy", policy => policy.RequireClaim("حذف طلب"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewOrderPloicy", policy => policy.RequireClaim("عرض الطلبات"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewOrderReportPloicy", policy => policy.RequireClaim("عرض تقرير الطلبات"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewOrderDetailsPloicy", policy => policy.RequireClaim("عرض تفاصيل طلب"));

            builder.Services.AddAuthorizationBuilder()
          .AddPolicy("addShippingSettingPloicy", policy => policy.RequireClaim("اضافة اعداد شحن"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editShippingSettingPloicy", policy => policy.RequireClaim("تعديل اعداد شحن"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("deleteShippingSettingPloicy", policy => policy.RequireClaim("حذف اعداد شحن"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewShippingSettingPloicy", policy => policy.RequireClaim("عرض اعدادات الشحن"));

           
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editWeightSettingPloicy", policy => policy.RequireClaim("تعديل اعداد الوزن"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewWeightSettingPloicy", policy => policy.RequireClaim("عرض عداد الوزن"));

            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("editVillageSettingPloicy", policy => policy.RequireClaim("تعديل اعداد القرية"));
            builder.Services.AddAuthorizationBuilder()
            .AddPolicy("viewVillageSettingPloicy", policy => policy.RequireClaim("عرض عداد القرية"));
            #endregion



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
