using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Softuni.Community.Data.Configurations;
using Softuni.Community.Data.Models;

namespace Softuni.Community.Data
{
    public class SuCDbContext : IdentityDbContext<CustomUser>
    {
        public SuCDbContext(DbContextOptions<SuCDbContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }

        //public DbSet<Category> Categories { get; set; }
        //
        //public DbSet<Tag> Tags { get; set; }
        //
        //public DbSet<Answer> Answers { get; set; }
        //
        //public DbSet<Question> Questions { get; set; }
        //
        //public DbSet<QuestionsTags> QuestionsTags { get; set; }
        //
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new QuestionsTagsConfig());
        //
        //    /* Used defaul identity configurations 
        //     * see more @ https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-2.2
        //     */
        //
        //    
        //}
    }
}