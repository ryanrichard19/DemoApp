# DemoApp Identity Server



Commands to build migrations for identity Server
 dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb

  dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationGrantDb

dotnet ef database update - c PersistedGrantDbContextdonet ef database update - c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext

dotnet ef migrations add InitialAspNetIdentityApplicationDbMigration -c ApplicationDbContext -o Data/Migrations/AspNetIdentity

```csharp
// Adding blazorwasm as a client with scopes
  configurationDBContext.Clients.Add(new Client
        {
            ClientId = "blazorwasm",
            AllowedGrantTypes = GrantTypes.Code,
            RequireClientSecret = false,
            AllowOfflineAccess = true,
            RedirectUris = { "https://localhost:5005/authentication/login-callback" },
            PostLogoutRedirectUris = { "https://localhost:5005/authentication/logout-callback" },
            AllowedCorsOrigins = { "https://localhost:5005" },


            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                "api"
            }
        }.ToEntity());
        configurationDBContext.SaveChanges();*/
// Adding angular as a client with scopes
         configurationDBContext.Clients.Add(new Client
         {
             ClientId = "angular",
             AllowedGrantTypes = GrantTypes.Code,
             RequireClientSecret = false,
             AllowOfflineAccess = true,

             RedirectUris = {"https://localhost:4200"},
             PostLogoutRedirectUris = { "https://localhost:4200" },
             AllowedCorsOrigins = { "https://localhost:4200" },


             AllowedScopes = new List<string>
             {
                 IdentityServerConstants.StandardScopes.OpenId,
                 IdentityServerConstants.StandardScopes.Profile,
                 IdentityServerConstants.StandardScopes.OfflineAccess,
                 "api"
             }
         }.ToEntity());
         configurationDBContext.SaveChanges();*/

         configurationDBContext.IdentityResources.Add(new IdentityResources.OpenId().ToEntity());
         configurationDBContext.IdentityResources.Add(new IdentityResources.Profile().ToEntity());
         configurationDBContext.SaveChanges();

// Adding web as a client with scopes
         configurationDBContext.Clients.Add(new Client
         {
             ClientId = "web",
             ClientSecrets = new List<Secret>
                  {
                      new Secret("secret".Sha256())
                  },
             AllowedGrantTypes = GrantTypes.Code,
             RedirectUris = new List<string>
             {
                 "https://localhost:5005/signin-oidc"
             },

             PostLogoutRedirectUris = new List<string>
             {
                 "https://localhost:5005/signout-oidc"
             },

             AllowedScopes = new List<string>
             {
                 IdentityServerConstants.StandardScopes.OpenId,
                 IdentityServerConstants.StandardScopes.Profile,
                 "api"
             }
         }.ToEntity());
         configurationDBContext.SaveChanges();

        // To seed test users and claims
        using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>())
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
        }

        //To Seed the identity server DB
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
         }
```
