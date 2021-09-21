using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

/*builder.Services.AddIdentityServer()
     .AddConfigurationStore(options =>
     {
         options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
             dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));
     })
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
             dbOpts => dbOpts.MigrationsAssembly(typeof(Program).Assembly.FullName));
    });*/

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
    });

var app = builder.Build();


app.UseIdentityServer();


/*using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
*//*    using (var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>())
    {
        persistedGrantDbContext.Database.Migrate();
    }*//*

    using (var configurationDBContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
    {
        if (!configurationDBContext.Clients.Any())
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
        }

    }
}*/

app.Run();
