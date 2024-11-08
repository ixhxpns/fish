using Blazored.LocalStorage;
using Blazored.SessionStorage;
using FISH.Areas.Identity;
using FISH.Data;
using FISH.Services;
using FISH.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FISH;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSignalR();
        builder.Services
            .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddBootstrapBlazor();
        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20); // 設置 Session 的過期時間
            //options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBootstrapBlazorHtml2PdfService();
        builder.Services.AddBootstrapBlazorWinBoxService();

        builder.Services.AddScoped<ILocalStorage, LocalStorage>();
        builder.Services.AddBootstrapBlazorTableExportService();
        builder.Services.Configure<HubOptions>
        (
            option =>
            {
                option.MaximumReceiveMessageSize = 1000000;
                option.EnableDetailedErrors = true;
                option.HandshakeTimeout = TimeSpan.FromSeconds(30);
                option.KeepAliveInterval = TimeSpan.FromSeconds(15);
            }
        );
        //builder.Services.AddBlazoredSessionStorage(config => {
        //    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        //    config.JsonSerializerOptions.IgnoreNullValues = true;
        //    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        //    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        //    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        //    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        //    config.JsonSerializerOptions.WriteIndented = false;
        //}
        //);

        //builder.Services.AddHttpClient("FishServerAPI", client => client.BaseAddress = new Uri("https://g-mate.org:8666/")).ConfigurePrimaryHttpMessageHandler(h =>
        builder.Services
            .AddHttpClient("FishServerAPI", client => client.BaseAddress = new Uri("https://g-mate.org:7777/"))
            .ConfigurePrimaryHttpMessageHandler(h =>
                //builder.Services.AddHttpClient("FishServerAPI", client => client.BaseAddress = new Uri("https://g-mate.org:7531/")).ConfigurePrimaryHttpMessageHandler(h =>
                //builder.Services.AddHttpClient("FishServerAPI", client => client.BaseAddress = new Uri("https://localhost:8668/")).ConfigurePrimaryHttpMessageHandler(h =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = delegate { return true; };
                return handler;
            });
        builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }


        app.UseSession(); // 啟用 Session

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();
        app.MapBlazorHub();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });

        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}