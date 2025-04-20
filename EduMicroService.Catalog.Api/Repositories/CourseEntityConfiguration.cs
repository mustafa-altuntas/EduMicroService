using EduMicroService.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace EduMicroService.Catalog.Api.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // collection or index/document/field -> tablo/satır/kolon
            builder.ToCollection("courses");
            builder.HasKey(c => c.Id);

            //property lerinden id değerini database de üretmesini istiyoruz
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(c => c.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(c => c.Created).HasElementName("created");
            builder.Property(c => c.UserId).HasElementName("userId");
            builder.Property(c => c.CategoryId).HasElementName("categoryId");
            builder.Property(c => c.Picture).HasElementName("picture");
            builder.Ignore(c => c.Category); // Category propertyini ignore ettik çünkü bu bir navigation propery ve categoryId ile ilişkilendireceğiz

            builder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(c => c.Duration).HasElementName("duration");
                feature.Property(c => c.Rating).HasElementName("rating");
                feature.Property(c => c.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
            });
        }
    }
}
