using IdentityApp.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Data
{
    public class ProjDbContext : IdentityDbContext<User>
    {
        public ProjDbContext(DbContextOptions<ProjDbContext> options) : base(options)
        {
            
        }
    }
}
