﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241213163129_isfoundermovedinitial")]
    partial class isfoundermovedinitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Domain.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("FoundingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegistryCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalCapital")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("IdCode")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Domain.Shareholder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonId1")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ShareholderBusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShareholderTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("PersonId");

                    b.HasIndex("PersonId1");

                    b.HasIndex("ShareholderBusinessId");

                    b.HasIndex("ShareholderTypeId");

                    b.ToTable("Shareholders");
                });

            modelBuilder.Entity("Domain.ShareholderInBusiness", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShareholderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("ShareholderId");

                    b.ToTable("ShareholdersInBusinesses");
                });

            modelBuilder.Entity("Domain.ShareholderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ShareholdersTypes");
                });

            modelBuilder.Entity("Domain.Shareholder", b =>
                {
                    b.HasOne("Domain.Business", null)
                        .WithMany("ShareholderPlaceInBusiness")
                        .HasForeignKey("BusinessId");

                    b.HasOne("Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.HasOne("Domain.Person", null)
                        .WithMany("Shareholders")
                        .HasForeignKey("PersonId1");

                    b.HasOne("Domain.Business", "ShareholderBusiness")
                        .WithMany()
                        .HasForeignKey("ShareholderBusinessId");

                    b.HasOne("Domain.ShareholderType", "ShareholderType")
                        .WithMany("Shareholders")
                        .HasForeignKey("ShareholderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("ShareholderBusiness");

                    b.Navigation("ShareholderType");
                });

            modelBuilder.Entity("Domain.ShareholderInBusiness", b =>
                {
                    b.HasOne("Domain.Business", "Business")
                        .WithMany("ShareholdersInBusiness")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Shareholder", "Shareholder")
                        .WithMany("ShareholdersInBusiness")
                        .HasForeignKey("ShareholderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Shareholder");
                });

            modelBuilder.Entity("Domain.Business", b =>
                {
                    b.Navigation("ShareholderPlaceInBusiness");

                    b.Navigation("ShareholdersInBusiness");
                });

            modelBuilder.Entity("Domain.Person", b =>
                {
                    b.Navigation("Shareholders");
                });

            modelBuilder.Entity("Domain.Shareholder", b =>
                {
                    b.Navigation("ShareholdersInBusiness");
                });

            modelBuilder.Entity("Domain.ShareholderType", b =>
                {
                    b.Navigation("Shareholders");
                });
#pragma warning restore 612, 618
        }
    }
}
