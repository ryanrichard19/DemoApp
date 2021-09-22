using DemoApp.IdentityServer.Data;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
    

builder.Services.AddIdentityServer()
      .AddConfigurationStore(options =>
      {
          options.ConfigureDbContext = builder =>
              builder.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));

      })
    // this adds the operational data from DB (codes, tokens, consents)
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext =
            builder => builder.UseSqlServer(connectionString, dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));
    })
    .AddAspNetIdentity<IdentityUser>();

builder.Services.AddRazorPages();


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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages(); ;
app.MapControllers();




using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    using (var configurationDBContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
    {
        // To seed test users and claims
        /*using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>())
        {
            var user = new IdentityUser("test@example.com")
            {
                Email = "test@example.com"
            };

            userManager.CreateAsync(user, "Pass123$").Wait();
            userManager.AddClaimsAsync(user, new List<Claim>
                            {
                                new(JwtClaimTypes.FamilyName, "Clark"),
                                new(JwtClaimTypes.GivenName, "Thomas")
                            }).Wait();
        }*/

        //To Seed the identity server DB
        /* if (!configurationDBContext.Clients.Any())
         {
             configurationDBContext.Clients.Add(new Client
             {
                 ClientId = "console",
                 ClientSecrets = new List<Secret>
                 {
                     new Secret("secret".Sha256())
                 },
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 AllowedScopes = new List<string>{
                     "api"
                 }
             }.ToEntity());

         configurationDBContext.Clients.Add(new Client
         {
             ClientId = "swagger",
             AllowedCorsOrigins = new List<string>
             {
                 "https://localhost:7179"
             }
         }.ToEntity());
             configurationDBContext.SaveChanges();
         }

         if (!configurationDBContext.ApiScopes.Any())
         {
             configurationDBContext.ApiScopes.Add(new ApiScope("api").ToEntity());
             configurationDBContext.SaveChanges();
         }

         if (!configurationDBContext.ApiResources.Any())
         {
             configurationDBContext.ApiResources.Add(new ApiResource("api")
             {
                 Scopes = new List<string>
                 {
                     "api"
                 }
             }.ToEntity());
             configurationDBContext.SaveChanges();
         }*/

    }
}

app.Run();
