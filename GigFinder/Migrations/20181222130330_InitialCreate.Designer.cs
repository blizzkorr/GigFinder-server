﻿// <auto-generated />
using System;
using GigFinder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GigFinder.Migrations
{
    [DbContext(typeof(GigFinderContext))]
    [Migration("20181222130330_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

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

                    b.Property<int>("LocationId");

                    b.Property<DateTime>("Start");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GigFinder.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int>("EventId");

                    b.Property<int>("HostId");

                    b.Property<int>("ParentId");

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

            modelBuilder.Entity("GigFinder.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressAddition");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("HouseNumber");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Street");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Locations");
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
                    b.Property<int>("EventId");

                    b.Property<int>("ArtistId");

                    b.Property<bool>("Accepted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Conditions");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("EventId", "ArtistId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("GigFinder.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<int>("ArtistId");

                    b.Property<int>("HostId");

                    b.Property<byte[]>("Image")
                        .IsRequired();

                    b.Property<bool>("IsProfileThumbnail")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("HostId");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("GigFinder.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int>("AuthorId");

                    b.Property<string>("Comment");

                    b.Property<int>("HostId");

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

                    b.Property<int>("LocationId");

                    b.Property<double>("Radius");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("LocationId");

                    b.ToTable("SearchRequests");
                });

            modelBuilder.Entity("GigFinder.Models.SocialMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("Thumbnail");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Website")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("GigFinder.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("GoogleIdToken")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("ProfilePicture");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("GigFinder.Models.Artist", b =>
                {
                    b.HasBaseType("GigFinder.Models.User");

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(6);

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasDiscriminator().HasValue("Artist");
                });

            modelBuilder.Entity("GigFinder.Models.Host", b =>
                {
                    b.HasBaseType("GigFinder.Models.User");

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .HasColumnName("Host_BackgroundColor")
                        .IsFixedLength(true)
                        .HasMaxLength(6);

                    b.Property<int>("DefaultLocationId");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("Host_Timestamp");

                    b.HasIndex("DefaultLocationId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Host");
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

                    b.HasOne("GigFinder.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
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
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.SearchRequest")
                        .WithMany("Genres")
                        .HasForeignKey("SearchRequestId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("GigFinder.Models.User", "Author")
                        .WithMany("SentMessages")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.User", "Receiver")
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
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Host", "Host")
                        .WithMany("Pictures")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Review", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("Reviews")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.User", "Author")
                        .WithMany("WrittenReviews")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Host", "Host")
                        .WithMany("Reviews")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.SearchRequest", b =>
                {
                    b.HasOne("GigFinder.Models.Artist", "Artist")
                        .WithMany("SearchRequests")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GigFinder.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GigFinder.Models.Host", b =>
                {
                    b.HasOne("GigFinder.Models.Location", "Location")
                        .WithOne()
                        .HasForeignKey("GigFinder.Models.Host", "DefaultLocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
