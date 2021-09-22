using Microsoft.EntityFrameworkCore;

namespace DemoApp.IdentityServer.Data
{
    public class IdentitydbContext
    {
        private readonly DbContextOptions options;

        public IdentitydbContext(DbContextOptions options)
        {
            this.options = options;
        }
    }
}