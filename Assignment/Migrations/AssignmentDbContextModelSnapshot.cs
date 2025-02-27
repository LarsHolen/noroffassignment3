﻿// <auto-generated />
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment.Migrations
{
    [DbContext(typeof(AssignmentDbContext))]
    partial class AssignmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Assignment.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Character");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "Ripley",
                            FullName = "Ellen Louise Ripley",
                            Gender = "Female"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "Rocky",
                            FullName = "Robert Balboa",
                            Gender = "Male"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "Dom",
                            FullName = "Dominic Toretto",
                            Gender = "Male"
                        },
                        new
                        {
                            Id = 4,
                            Alias = "",
                            FullName = "Brian O'Conner",
                            Gender = "Male"
                        });
                });

            modelBuilder.Entity("Assignment.Models.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Franchise");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "No known franchise or missing information",
                            Name = "No franchise"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Fast cars and action!",
                            Name = "Fast And Furious"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Boxing",
                            Name = "Rocky"
                        },
                        new
                        {
                            Id = 4,
                            Description = "SciFi, alien monsters attack!",
                            Name = "Alien"
                        });
                });

            modelBuilder.Entity("Assignment.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("FranchiseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Genre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ImageURL")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ReleaseYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrailerURL")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "Rob Cohen",
                            FranchiseId = 2,
                            Genre = "Action, crime, thriller",
                            ImageURL = "https://www.imdb.com/title/tt0232500/mediaviewer/rm3153527296/?ref_=ext_shr_lnk",
                            ReleaseYear = "2001",
                            Title = "Fast And furious 1",
                            TrailerURL = "https://www.imdb.com/video/vi2048898073?playlistId=tt0232500"
                        },
                        new
                        {
                            Id = 2,
                            Director = "Rob Cohen",
                            FranchiseId = 2,
                            Genre = "Action, crime, thriller",
                            ImageURL = "https://www.imdb.com/title/tt0232500/mediaviewer/rm3153527296/?ref_=ext_shr_lnk",
                            ReleaseYear = "2002",
                            Title = "Fast And furious 2",
                            TrailerURL = "https://www.imdb.com/video/vi2048898073?playlistId=tt0232500"
                        },
                        new
                        {
                            Id = 3,
                            Director = "John G. Avildsen",
                            FranchiseId = 3,
                            Genre = "Action, sport, boxing",
                            ImageURL = "https://www.imdb.com/title/tt0075148/mediaviewer/rm960529408/?ref_=ext_shr_lnk",
                            ReleaseYear = "1976",
                            Title = "Rocky",
                            TrailerURL = "https://www.imdb.com/video/vi2997093657?playlistId=tt0075148"
                        },
                        new
                        {
                            Id = 4,
                            Director = "Sylvester Stallone",
                            FranchiseId = 3,
                            Genre = "Action, sport, boxing",
                            ImageURL = "https://www.imdb.com/title/tt0075148/mediaviewer/rm960529408/?ref_=ext_shr_lnk",
                            ReleaseYear = "1979",
                            Title = "Rocky 2",
                            TrailerURL = "https://www.imdb.com/video/vi2997093657?playlistId=tt0075148"
                        },
                        new
                        {
                            Id = 5,
                            Director = "Ridley Scott",
                            FranchiseId = 4,
                            Genre = "Horror, Sci-Fi",
                            ImageURL = "https://www.imdb.com/title/tt0078748/mediaviewer/rm2990766080/?ref_=ext_shr_lnk",
                            ReleaseYear = "1979",
                            Title = "Alien",
                            TrailerURL = "https://www.imdb.com/video/vi1497801241?playlistId=tt0078748"
                        },
                        new
                        {
                            Id = 6,
                            Director = "James Cameron",
                            FranchiseId = 4,
                            Genre = "Horror, Sci-Fi",
                            ImageURL = "https://www.imdb.com/title/tt0078748/mediaviewer/rm2990766080/?ref_=ext_shr_lnk",
                            ReleaseYear = "1986",
                            Title = "Aliens",
                            TrailerURL = "https://www.imdb.com/video/vi1497801241?playlistId=tt0078748"
                        });
                });

            modelBuilder.Entity("CharacterMovie", b =>
                {
                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.Property<int>("CharactersId")
                        .HasColumnType("int");

                    b.HasKey("MoviesId", "CharactersId");

                    b.HasIndex("CharactersId");

                    b.ToTable("CharacterMovie");

                    b.HasData(
                        new
                        {
                            MoviesId = 3,
                            CharactersId = 2
                        },
                        new
                        {
                            MoviesId = 4,
                            CharactersId = 2
                        },
                        new
                        {
                            MoviesId = 5,
                            CharactersId = 1
                        },
                        new
                        {
                            MoviesId = 6,
                            CharactersId = 1
                        },
                        new
                        {
                            MoviesId = 1,
                            CharactersId = 3
                        },
                        new
                        {
                            MoviesId = 1,
                            CharactersId = 4
                        },
                        new
                        {
                            MoviesId = 2,
                            CharactersId = 3
                        },
                        new
                        {
                            MoviesId = 2,
                            CharactersId = 4
                        });
                });

            modelBuilder.Entity("Assignment.Models.Movie", b =>
                {
                    b.HasOne("Assignment.Models.Franchise", "Franchise")
                        .WithMany("Movies")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("CharacterMovie", b =>
                {
                    b.HasOne("Assignment.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assignment.Models.Franchise", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
