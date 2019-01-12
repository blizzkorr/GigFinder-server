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
            modelBuilder.ApplyConfiguration(new FavoriteConfiguration());
            modelBuilder.ApplyConfiguration(new HostGenreConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistGenreConfiguration());
            modelBuilder.ApplyConfiguration(new EventGenreConfiguration());
            modelBuilder.ApplyConfiguration(new SearchRequestGenreConfiguration());
        }

        public DbSet<UserID> UserIDs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<SearchRequest> SearchRequests { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<ArtistSocialMedia> ArtistSocialMedias { get; set; }
        public DbSet<HostSocialMedia> HostSocialMedias { get; set; }
        public DbSet<ArtistGenre> ArtistGenres { get; set;}
        public DbSet<HostGenre> HostGenres { get; set; }
        public DbSet<EventGenre> EventGenres { get; set; }
        public DbSet<SearchRequestGenre> SearchRequestGenres { get; set; }
    }
}
