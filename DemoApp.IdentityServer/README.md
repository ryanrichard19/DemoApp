# DemoApp Identity Server



Commands to build migrations for identity Server
 dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb

  dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationGrantDb

dotnet ef database update - c PersistedGrantDbContextdonet ef database update - c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext