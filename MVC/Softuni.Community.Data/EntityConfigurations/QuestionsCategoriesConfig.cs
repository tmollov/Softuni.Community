using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softuni.Community.Data.Models;

namespace Softuni.Community.Data.EntityConfigurations
{
    public class QuestionsCategoriesConfig : IEntityTypeConfiguration<QuestionsCategories>
    {
        public void Configure(EntityTypeBuilder<QuestionsCategories> builder)
        {
            builder.HasKey(x => new { x.QuestionId, x.CategoryId });
        }
    }
}