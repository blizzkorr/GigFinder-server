using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GigFinder.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Genres_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserIDs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoogleIdToken = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIDs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Tag = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_UserIDs_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "UserIDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_UserIDs_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "UserIDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistGenres",
                columns: table => new
                {
                    ArtistId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistGenres", x => new { x.ArtistId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_ArtistGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistSocialMedias",
                columns: table => new
                {
                    ArtistId = table.Column<int>(nullable: false),
                    SocialMediaId = table.Column<int>(nullable: false),
                    Handle = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSocialMedias", x => new { x.ArtistId, x.SocialMediaId });
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistId = table.Column<int>(nullable: false),
                    HostId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventId = table.Column<int>(nullable: false),
                    ArtistId = table.Column<int>(nullable: false),
                    Conditions = table.Column<string>(nullable: true),
                    Accepted = table.Column<bool>(nullable: false, defaultValue: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Image = table.Column<byte[]>(nullable: false),
                    ArtistId = table.Column<int>(nullable: true),
                    HostId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProfilePictureId = table.Column<int>(nullable: true),
                    BackgroundColor = table.Column<string>(fixedLength: true, maxLength: 6, nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_UserIDs_Id",
                        column: x => x.Id,
                        principalTable: "UserIDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artists_Pictures_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProfilePictureId = table.Column<int>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    BackgroundColor = table.Column<string>(fixedLength: true, maxLength: 6, nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hosts_UserIDs_Id",
                        column: x => x.Id,
                        principalTable: "UserIDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hosts_Pictures_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Website = table.Column<string>(nullable: false),
                    ThumbnailId = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedias_Pictures_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistId = table.Column<int>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Radius = table.Column<double>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchRequests_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    HostId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Hosts_HostId",
                        column: x => x.HostId,
                        principalTable: "Hosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGenres",
                columns: table => new
                {
                    HostId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGenres", x => new { x.HostId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_HostGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostGenres_Hosts_HostId",
                        column: x => x.HostId,
                        principalTable: "Hosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ArtistId = table.Column<int>(nullable: true),
                    HostId = table.Column<int>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_UserIDs_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "UserIDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Hosts_HostId",
                        column: x => x.HostId,
                        principalTable: "Hosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HostSocialMedias",
                columns: table => new
                {
                    HostId = table.Column<int>(nullable: false),
                    SocialMediaId = table.Column<int>(nullable: false),
                    Handle = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostSocialMedias", x => new { x.HostId, x.SocialMediaId });
                    table.ForeignKey(
                        name: "FK_HostSocialMedias_Hosts_HostId",
                        column: x => x.HostId,
                        principalTable: "Hosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostSocialMedias_SocialMedias_SocialMediaId",
                        column: x => x.SocialMediaId,
                        principalTable: "SocialMedias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchRequestGenres",
                columns: table => new
                {
                    SearchRequestId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequestGenres", x => new { x.SearchRequestId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_SearchRequestGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SearchRequestGenres_SearchRequests_SearchRequestId",
                        column: x => x.SearchRequestId,
                        principalTable: "SearchRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventGenres",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGenres", x => new { x.EventId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_EventGenres_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistGenres_GenreId",
                table: "ArtistGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_ProfilePictureId",
                table: "Artists",
                column: "ProfilePictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSocialMedias_SocialMediaId",
                table: "ArtistSocialMedias",
                column: "SocialMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGenres_GenreId",
                table: "EventGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_HostId",
                table: "Events",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ArtistId",
                table: "Favorites",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_HostId",
                table: "Favorites",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ParentId",
                table: "Genres",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGenres_GenreId",
                table: "HostGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_ProfilePictureId",
                table: "Hosts",
                column: "ProfilePictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostSocialMedias_SocialMediaId",
                table: "HostSocialMedias",
                column: "SocialMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_ArtistId",
                table: "Participations",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_EventId",
                table: "Participations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ArtistId",
                table: "Pictures",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_EventId",
                table: "Pictures",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_HostId",
                table: "Pictures",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ArtistId",
                table: "Reviews",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AuthorId",
                table: "Reviews",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HostId",
                table: "Reviews",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchRequestGenres_GenreId",
                table: "SearchRequestGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchRequests_ArtistId",
                table: "SearchRequests",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_ThumbnailId",
                table: "SocialMedias",
                column: "ThumbnailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistGenres_Artists_ArtistId",
                table: "ArtistGenres",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSocialMedias_Artists_ArtistId",
                table: "ArtistSocialMedias",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSocialMedias_SocialMedias_SocialMediaId",
                table: "ArtistSocialMedias",
                column: "SocialMediaId",
                principalTable: "SocialMedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Artists_ArtistId",
                table: "Favorites",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Hosts_HostId",
                table: "Favorites",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Artists_ArtistId",
                table: "Participations",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Events_EventId",
                table: "Participations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Artists_ArtistId",
                table: "Pictures",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Events_EventId",
                table: "Pictures",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Hosts_HostId",
                table: "Pictures",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Artists_ArtistId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_UserIDs_Id",
                table: "Hosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_Pictures_ProfilePictureId",
                table: "Hosts");

            migrationBuilder.DropTable(
                name: "ArtistGenres");

            migrationBuilder.DropTable(
                name: "ArtistSocialMedias");

            migrationBuilder.DropTable(
                name: "EventGenres");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "HostGenres");

            migrationBuilder.DropTable(
                name: "HostSocialMedias");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SearchRequestGenres");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "SearchRequests");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "UserIDs");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Hosts");
        }
    }
}
