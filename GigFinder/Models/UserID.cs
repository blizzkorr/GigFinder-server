using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace GigFinder.Models
{
    public class UserID 
    {
        public int Id { get; set; }
        public string GoogleIdToken { get; set; }
        public string DeviceToken { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Host Host { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Review> WrittenReviews { get; set; }

        public UserID()
        {
            SentMessages = new HashSet<Message>();
            ReceivedMessages = new HashSet<Message>();
            WrittenReviews = new HashSet<Review>();
        }

        public void Anonymize()
        {
            GoogleIdToken = "";
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<UserID>
    {
        public void Configure(EntityTypeBuilder<UserID> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.GoogleIdToken).IsRequired();

            builder.HasOne(u => u.Artist).WithOne(a => a.UserId).HasForeignKey<Artist>(a => a.Id);
            builder.HasOne(u => u.Host).WithOne(h => h.UserId).HasForeignKey<Host>(h => h.Id);
        }
    }
}