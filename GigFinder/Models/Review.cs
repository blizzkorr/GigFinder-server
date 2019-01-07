using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Review 
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int ArtistId { get; set; }
        public int HostId { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual UserID Author { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Host Host { get; set; }

        public Review() { }
    }

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating).IsRequired();
            builder.Property(r => r.Timestamp).IsRowVersion();

            builder.HasOne(r => r.Author).WithMany(u => u.WrittenReviews).HasForeignKey(r => r.AuthorId).IsRequired();
            builder.HasOne(r => r.Artist).WithMany(a => a.Reviews).HasForeignKey(r => r.ArtistId);
            builder.HasOne(r => r.Host).WithMany(h => h.Reviews).HasForeignKey(r => r.HostId);
        }
    }
}