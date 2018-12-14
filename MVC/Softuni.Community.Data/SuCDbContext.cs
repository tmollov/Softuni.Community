using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data.Models;

namespace Softuni.Community.Data
{
    public class SuCDbContext : IdentityDbContext<CustomUser>
    {
        public SuCDbContext(DbContextOptions<SuCDbContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }
    }
}