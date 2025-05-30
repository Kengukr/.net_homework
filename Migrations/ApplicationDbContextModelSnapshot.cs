﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using task_new.Data;

#nullable disable

namespace task_new.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("task_new.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BirthYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthYear = 1814,
                            Name = "Тарас Шевченко"
                        },
                        new
                        {
                            Id = 2,
                            BirthYear = 1856,
                            Name = "Іван Франко"
                        },
                        new
                        {
                            Id = 3,
                            BirthYear = 1871,
                            Name = "Леся Українка"
                        });
                });

            modelBuilder.Entity("task_new.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Title = "Кобзар",
                            Year = 1840
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Title = "Гайдамаки",
                            Year = 1841
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Title = "Захар Беркут",
                            Year = 1883
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 2,
                            Title = "Украдене щастя",
                            Year = 1893
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 3,
                            Title = "Лісова пісня",
                            Year = 1911
                        });
                });

            modelBuilder.Entity("task_new.Models.Book", b =>
                {
                    b.HasOne("task_new.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("task_new.Models.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
