using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public int? ThumbnailId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Picture Thumbnail { get; set; }

        public SocialMedia() { }
    }

    public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
    {
        public void Configure(EntityTypeBuilder<SocialMedia> builder)
        {
            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Name).IsRequired();
            builder.Property(sm => sm.Website).IsRequired();
            builder.Property(sm => sm.Timestamp).IsRowVersion();

            builder.HasOne(sm => sm.Thumbnail).WithOne().HasForeignKey<SocialMedia>(sm => sm.ThumbnailId);
        }
    }
}