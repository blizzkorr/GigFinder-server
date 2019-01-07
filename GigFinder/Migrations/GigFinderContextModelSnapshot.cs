﻿// <auto-generated />
using System;
using GigFinder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GigFinder.Migrations
{
    [DbContext(typeof(GigFinderContext))]
    partial class GigFinderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity("GigFinder.Models.Artist", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(6);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ProfilePictureId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureId")
                        .IsUnique();

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("GigFinder.Models.ArtistSocialMedia", b =>
                {
                    b.Property<int>("ArtistId");

                    b.Property<int>("SocialMediaId");

                    b.Property<string>("Handle")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("ArtistId", "SocialMediaId");

                    b.HasIndex("SocialMediaId");

                    b.ToTable("ArtistSocialMedias");
                });

            modelBuilder.Entity("GigFinder.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("End");

                    b.Property<int>("HostId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTime>("Start");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GigFinder.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int>("HostId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("HostId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("GigFinder.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int>("EventId");

                    b.Property<int>("HostId");

                    b.Property<int?>("ParentId");

                    b.Property<int>("SearchRequestId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("EventId");

                    b.HasIndex("HostId");

                    b.HasIndex("ParentId");

                    b.HasIndex("SearchRequestId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("GigFinder.Models.Host", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(6);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ProfilePictureId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureId")
                        .IsUnique();

                    b.ToTable("Hosts");
                });

            modelBuilder.Entity("GigFinder.Models.HostSocialMedia", b =>
                {
                    b.Property<int>("HostId");

                    b.Property<int>("SocialMediaId");

                    b.Property<string>("Handle")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("HostId", "SocialMediaId");

                    b.HasIndex("SocialMediaId");

                    b.ToTable("HostSocialMedias");
                });

            modelBuilder.Entity("GigFinder.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("ReceiverId");

                    b.Property<string>("Tag");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("GigFinder.Models.Participation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("ArtistId");

                    b.Property<string>("Conditions");

                    b.Property<int>("EventId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("EventId");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("GigFinder.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArtistId");

                    b.Property<int?>("EventId");

                    b.Property<int?>("HostId");

                    b.Property<byte[]>("Image")
                        .IsRequired();

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("EventId");

                    b.HasIndex("HostId");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("GigFinder.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArtistId");

                    b.Property<int>("AuthorId");

                    b.Property<string>("Comment");

                    b.Property<int?>("HostId");

                    b.Property<int>("Rating");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("HostId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("GigFinder.Models.SearchRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<double>("Radius");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("SearchRequests");
                });

            modelBuilder.Entity("GigFinder.Models.SocialMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ThumbnailId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Website")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ThumbnailId")
                        .IsUnique();

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("GigFinder.Models.UserID", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GoogleIdToken")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UserIDs");
                });

            modelBuilder.Entity("GigFinder.Models.Artist", b =>
                {
                    b.HasOne("GigFinder.Models.UserID", "UserId")
                        .WithOne("Artist")
                        .HasForeignKey("GigFinder.Models.Artist", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Picture", "ProfilePicture")
                        .WithOne()
                        .HasForeignKey("GigFinder.Models.Artist", "ProfilePictureId");
                });

            modelBuilder.Entity("GigFinder.Models.ArtistSocialMedia", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("ArtistSocialMedias")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.SocialMedia", "SocialMedia")
                        .WithMany()
                        .HasForeignKey("SocialMediaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Event", b =>
                {
                    b.HasOne("GigFinder.Models.Host", "Host")
                        .WithMany("Events")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Favorite", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("Favorites")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Host", "Host")
                        .WithMany()
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Genre", b =>
                {
                    b.HasOne("GigFinder.Models.Artist")
                        .WithMany("Genres")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Event")
                        .WithMany("Genres")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Host")
                        .WithMany("DefaultGenres")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Genre", "Parent")
                        .WithMany("SubGenres")
                        .HasForeignKey("ParentId");

                    b.HasOne("GigFinder.Models.SearchRequest")
                        .WithMany("Genres")
                        .HasForeignKey("SearchRequestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Host", b =>
                {
                    b.HasOne("GigFinder.Models.UserID", "UserId")
                        .WithOne("Host")
                        .HasForeignKey("GigFinder.Models.Host", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Picture", "ProfilePicture")
                        .WithOne()
                        .HasForeignKey("GigFinder.Models.Host", "ProfilePictureId");
                });

            modelBuilder.Entity("GigFinder.Models.HostSocialMedia", b =>
                {
                    b.HasOne("GigFinder.Models.Host", "Host")
                        .WithMany("HostSocialMedias")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.SocialMedia", "SocialMedia")
                        .WithMany()
                        .HasForeignKey("SocialMediaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Message", b =>
                {
                    b.HasOne("GigFinder.Models.UserID", "Author")
                        .WithMany("SentMessages")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.UserID", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Participation", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("Participations")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Event", "Event")
                        .WithMany("Participations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Picture", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("Pictures")
                        .HasForeignKey("ArtistId");

                    b.HasOne("GigFinder.Models.Event", "Event")
                        .WithMany("Pictures")
                        .HasForeignKey("EventId");

                    b.HasOne("GigFinder.Models.Host", "Host")
                        .WithMany("Pictures")
                        .HasForeignKey("HostId");
                });

            modelBuilder.Entity("GigFinder.Models.Review", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("Reviews")
                        .HasForeignKey("ArtistId");

                    b.HasOne("GigFinder.Models.UserID", "Author")
                        .WithMany("WrittenReviews")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Host", "Host")
                        .WithMany("Reviews")
                        .HasForeignKey("HostId");
                });

            modelBuilder.Entity("GigFinder.Models.SearchRequest", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("SearchRequests")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.SocialMedia", b =>
                {
                    b.HasOne("GigFinder.Models.Picture", "Thumbnail")
                        .WithOne()
                        .HasForeignKey("GigFinder.Models.SocialMedia", "ThumbnailId");
                });
#pragma warning restore 612, 618
        }
    }
}
