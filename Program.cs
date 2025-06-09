using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Implementations;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Implementations;
using Bookify.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Hangfire;
using Hangfire.SqlServer;
using StackExchange.Profiling;


namespace Bookify
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            #region Services
            builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ITicketTypeService, TicketTypeService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IBookingDetailService, BookingDetailService>();
            builder.Services.AddScoped<ITemporaryBookingService, TemporaryBookingService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            #endregion

            #region Repositories
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            builder.Services.AddScoped<ITemporaryBookingRepository, TemporaryBookingRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingDetailsRepository, BookingDetailsRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            string? ConnectionString = builder.Configuration.GetConnectionString("Cs");


            // Use SQL Server distributed cache for session storage
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession();

            builder.Services.AddHangfire(config =>
                config.UseSqlServerStorage(ConnectionString, new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                }));

            builder.Services.AddHangfireServer();

            builder.Services.Configure<IdentityOptions>(options => { options.User.RequireUniqueEmail = true; });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(ConnectionString)
                       .EnableSensitiveDataLogging()
                       .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            });



            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseHangfireDashboard();

           RecurringJob.AddOrUpdate<CleanupTasks>("delete-temp-bookings",
                x => x.RemoveExpiredTemporaryBookings(), Cron.MinuteInterval(1));

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=EventCategories}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
