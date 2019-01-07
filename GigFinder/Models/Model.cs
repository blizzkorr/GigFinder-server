using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GigFinder.Models
{
    public class GigFinderContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GigFinder.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new PictureConfiguration());
            modelBuilder.ApplyConfiguration(new SocialMediaConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistSocialMediaConfiguration());
            modelBuilder.ApplyConfiguration(new HostConfiguration());
            modelBuilder.ApplyConfiguration(new HostSocialMediaConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipationConfiguration());
            modelBuilder.ApplyConfiguration(new SearchRequestConfiguration());
        }

        public DbSet<UserID> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SearchRequest> SearchRequests { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<ArtistSocialMedia> ArtistSocialMedias { get; set; }
        public DbSet<HostSocialMedia> HostSocialMedias { get; set; }
    }
}
