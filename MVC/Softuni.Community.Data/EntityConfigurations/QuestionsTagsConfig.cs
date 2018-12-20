using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softuni.Community.Data.Models;

namespace Softuni.Community.Data.EntityConfigurations
{
    internal class QuestionsTagsConfig : IEntityTypeConfiguration<QuestionsTags>
    {
        public void Configure(EntityTypeBuilder<QuestionsTags> builder)
        {
            builder
                .HasKey(x => new { x.TagId, x.QuestionId });

            //builder
            //    .HasOne(x => x.Question)
            //    .WithMany(x => x.Tags)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
