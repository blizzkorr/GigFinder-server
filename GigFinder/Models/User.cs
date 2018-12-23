using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace GigFinder.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string GoogleIdToken { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Review> WrittenReviews { get; set; }

        public User()
        {
            SentMessages = new HashSet<Message>();
            ReceivedMessages = new HashSet<Message>();
            WrittenReviews = new HashSet<Review>();
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.GoogleIdToken).IsRequired();
        }
    }
}