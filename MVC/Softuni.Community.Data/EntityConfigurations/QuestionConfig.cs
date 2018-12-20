using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softuni.Community.Data.Models;

namespace Softuni.Community.Data.EntityConfigurations
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .HasMany(x => x.Tags)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
