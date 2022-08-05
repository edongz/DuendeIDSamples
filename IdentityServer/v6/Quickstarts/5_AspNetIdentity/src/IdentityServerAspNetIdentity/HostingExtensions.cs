using IdentityServerAspNetIdentity.Data;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServerAspNetIdentity;

internal static class HostingExtensions
{
  public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
  {
    string migrationsAssembly = typeof(Program).Assembly.GetName().Name;
    builder.Services.AddRazorPages();

    string connectionString = builder.Configuration.GetConnectionString("sql_server_172");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    //注册时的配置选项 去掉密码复杂性要求
    builder.Services.Configure<IdentityOptions>(options =>
    {
      // Password settings.
      options.Password.RequireDigit = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;
      //options.Password.RequiredLength = 6;
      options.Password.RequiredUniqueChars = 1;

      // Lockout settings.
      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
      options.Lockout.MaxFailedAccessAttempts = 5;
      options.Lockout.AllowedForNewUsers = true;

      // User settings.

      options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
      //options.User.AllowedUserNameCharacters = null;
      options.User.RequireUniqueEmail = true;

      options.SignIn.RequireConfirmedEmail = false;
      options.SignIn.RequireConfirmedPhoneNumber = false;
    });

    builder.Services
        .AddIdentityServer(options =>
        {
          options.Events.RaiseErrorEvents = true;
          options.Events.RaiseInformationEvents = true;
          options.Events.RaiseFailureEvents = true;
          options.Events.RaiseSuccessEvents = true;

          // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
          options.EmitStaticAudienceClaim = true;
        })
      .AddConfigurationStore(options =>
      {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
      })
        .AddOperationalStore(options =>
        {
          options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                  sql => sql.MigrationsAssembly(migrationsAssembly));
        })
        .AddAspNetIdentity<ApplicationUser>()
        .AddProfileService<CustomProfileService>();

    //builder.Services.AddAuthentication()
    //    .AddGoogle(options =>
    //    {
    //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

    //        // register your IdentityServer with Google at https://console.developers.google.com
    //        // enable the Google+ API
    //        // set the redirect URI to https://localhost:5001/signin-google
    //        options.ClientId = "copy client ID from Google here";
    //        options.ClientSecret = "copy client secret from Google here";
    //    });

    return builder.Build();
  }

  public static WebApplication ConfigurePipeline(this WebApplication app)
  {
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseIdentityServer();
    app.UseAuthorization();

    app.MapRazorPages()
        .RequireAuthorization();

    return app;
  }
}
