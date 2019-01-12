using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class Message 
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public DateTime Created { get; set; }
        public byte[] Timestamp { get; set; }

        public UserID Author { get; set; }
        public UserID Receiver { get; set; }

        public Message() { }
    }

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Content).IsRequired();
            builder.Property(m => m.Created).HasDefaultValueSql("DATETIME('NOW')").IsRequired();
            builder.Property(m => m.Timestamp).IsRowVersion();

            builder.HasOne(m => m.Author).WithMany(u => u.SentMessages).HasForeignKey(m => m.AuthorId).IsRequired();
            builder.HasOne(m => m.Receiver).WithMany(u => u.ReceivedMessages).HasForeignKey(m => m.ReceiverId).IsRequired();
        }
    }
}