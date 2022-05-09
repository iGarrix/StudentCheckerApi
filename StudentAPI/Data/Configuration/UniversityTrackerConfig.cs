using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentAPI.Entities;

namespace StudentAPI.Data.Configuration
{
    public class UniversityTrackerConfig : IEntityTypeConfiguration<UniversityTracker>
    {
        public void Configure(EntityTypeBuilder<UniversityTracker> builder)
        {
            builder.HasKey(x => x.Id);

        }
    }
}
