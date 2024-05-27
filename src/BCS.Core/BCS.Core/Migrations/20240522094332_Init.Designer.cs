﻿// <auto-generated />
using System;
using BCS.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BCS.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240522094332_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BCS.Core.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("96c38101-a1b4-4360-8544-b40122769292"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "73c60151-6779-48b1-a56c-4111f43a99da",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Власник сайту",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@GMAIL.COM",
                            NormalizedUserName = "ADMIN@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEEvXGqJebHOhjHkMVMmM1ZWcAY02ktIc/jCMOL/DIjOd6nUoMb8whvDfU0qfTCKUZQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ed8963f7-a5ac-4c0e-b111-177f4adc645f",
                            TwoFactorEnabled = false,
                            UserName = "admin@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("82a5197e-c9d5-4458-9f18-07e3c0c53a81"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bcbb091d-3274-4420-a7e0-c716fe8a443b",
                            Email = "vlad.dzemyuk@gmail.com",
                            EmailConfirmed = true,
                            FullName = "Владислав Дзем'юк",
                            LockoutEnabled = false,
                            NormalizedEmail = "VLAD.DZEMYUK@GMAIL.COM",
                            NormalizedUserName = "VLAD.DZEMYUK@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGK67XF1uTUSiiOnAse18HIkPckmt3JMZi3kLqP+Ccxu6CrhF4sFB+LludIiAF8elw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "11d67fc4-ed05-4cbf-b682-8869ae647aea",
                            TwoFactorEnabled = false,
                            UserName = "vlad.dzemyuk@gmail.com"
                        });
                });

            modelBuilder.Entity("BCS.Core.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("city");
                });

            modelBuilder.Entity("BCS.Core.Entities.Complaint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Number")
                        .HasColumnType("longtext");

                    b.Property<string>("Photo")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Sdatetime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StreetId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StructureId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("StatusId");

                    b.HasIndex("StreetId");

                    b.HasIndex("StructureId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("complaints");
                });

            modelBuilder.Entity("BCS.Core.Entities.ComplaintComments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ComCommentData")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ComCommentPhoto")
                        .HasColumnType("longtext");

                    b.Property<Guid>("ComplaintId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ComplaintId");

                    b.HasIndex("UserId");

                    b.ToTable("complaintComments");
                });

            modelBuilder.Entity("BCS.Core.Entities.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("status");
                });

            modelBuilder.Entity("BCS.Core.Entities.Street", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("street");
                });

            modelBuilder.Entity("BCS.Core.Entities.Structure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("structure");
                });

            modelBuilder.Entity("BCS.Core.Entities.Suggestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Number")
                        .HasColumnType("longtext");

                    b.Property<string>("Photo")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Sdatetime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StreetId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StructureId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("StatusId");

                    b.HasIndex("StreetId");

                    b.HasIndex("StructureId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("suggestions");
                });

            modelBuilder.Entity("BCS.Core.Entities.SuggestionComments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("SugCommentData")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SugCommentPhoto")
                        .HasColumnType("longtext");

                    b.Property<Guid>("SuggestionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionId");

                    b.HasIndex("UserId");

                    b.ToTable("suggestionComments");
                });

            modelBuilder.Entity("BCS.Core.Entities.Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("type");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("18990c3c-a576-4dcb-bd16-f4d1a5e41861"),
                            ConcurrencyStamp = "18990c3c-a576-4dcb-bd16-f4d1a5e41861",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("22c5e8c9-745c-45f1-b6a4-9df6ce41c40d"),
                            ConcurrencyStamp = "22c5e8c9-745c-45f1-b6a4-9df6ce41c40d",
                            Name = "Worker",
                            NormalizedName = "WORKER"
                        },
                        new
                        {
                            Id = new Guid("6cc3b719-aa10-49a0-8746-ace9486a0c3c"),
                            ConcurrencyStamp = "6cc3b719-aa10-49a0-8746-ace9486a0c3c",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("96c38101-a1b4-4360-8544-b40122769292"),
                            RoleId = new Guid("18990c3c-a576-4dcb-bd16-f4d1a5e41861")
                        },
                        new
                        {
                            UserId = new Guid("96c38101-a1b4-4360-8544-b40122769292"),
                            RoleId = new Guid("22c5e8c9-745c-45f1-b6a4-9df6ce41c40d")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BCS.Core.Entities.Complaint", b =>
                {
                    b.HasOne("BCS.Core.Entities.City", "City")
                        .WithMany("Complaints")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Status", "Status")
                        .WithMany("Complaints")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Street", "Street")
                        .WithMany("Complaints")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Structure", "Structure")
                        .WithMany("Complaints")
                        .HasForeignKey("StructureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Type", "Type")
                        .WithMany("Complaints")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.AppUser", "User")
                        .WithMany("Complaints")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Status");

                    b.Navigation("Street");

                    b.Navigation("Structure");

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BCS.Core.Entities.ComplaintComments", b =>
                {
                    b.HasOne("BCS.Core.Entities.Complaint", "Complaint")
                        .WithMany("ComplaintCommentses")
                        .HasForeignKey("ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.AppUser", "User")
                        .WithMany("ComplaintCommentses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BCS.Core.Entities.Suggestion", b =>
                {
                    b.HasOne("BCS.Core.Entities.City", "City")
                        .WithMany("Suggestions")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Status", "Status")
                        .WithMany("Suggestions")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Street", "Street")
                        .WithMany("Suggestions")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Structure", "Structure")
                        .WithMany("Suggestions")
                        .HasForeignKey("StructureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.Type", "Type")
                        .WithMany("Suggestions")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.AppUser", "User")
                        .WithMany("Suggestions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Status");

                    b.Navigation("Street");

                    b.Navigation("Structure");

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BCS.Core.Entities.SuggestionComments", b =>
                {
                    b.HasOne("BCS.Core.Entities.Suggestion", "Suggestion")
                        .WithMany("SuggestionCommentses")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.AppUser", "User")
                        .WithMany("SuggestionCommentses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BCS.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BCS.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BCS.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("BCS.Core.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BCS.Core.Entities.AppUser", b =>
                {
                    b.Navigation("ComplaintCommentses");

                    b.Navigation("Complaints");

                    b.Navigation("SuggestionCommentses");

                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("BCS.Core.Entities.City", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("BCS.Core.Entities.Complaint", b =>
                {
                    b.Navigation("ComplaintCommentses");
                });

            modelBuilder.Entity("BCS.Core.Entities.Status", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("BCS.Core.Entities.Street", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("BCS.Core.Entities.Structure", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("BCS.Core.Entities.Suggestion", b =>
                {
                    b.Navigation("SuggestionCommentses");
                });

            modelBuilder.Entity("BCS.Core.Entities.Type", b =>
                {
                    b.Navigation("Complaints");

                    b.Navigation("Suggestions");
                });
#pragma warning restore 612, 618
        }
    }
}
