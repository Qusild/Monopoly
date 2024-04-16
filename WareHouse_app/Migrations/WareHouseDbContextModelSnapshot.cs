﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WareHouse_app.Migrations
{
    [DbContext(typeof(WareHouseDbContext))]
    partial class WareHouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.Box", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("CreationDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("ExperationDate")
                        .HasColumnType("date");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<double>("Length")
                        .HasColumnType("double precision");

                    b.Property<Guid>("PalletId")
                        .HasColumnType("uuid");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PalletId");

                    b.ToTable("Boxes", (string)null);
                });

            modelBuilder.Entity("Models.Pallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("ExperationDate")
                        .HasColumnType("date");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<double>("Length")
                        .HasColumnType("double precision");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Pallets", (string)null);
                });

            modelBuilder.Entity("Models.Box", b =>
                {
                    b.HasOne("Models.Pallet", "Pallet")
                        .WithMany("Boxes")
                        .HasForeignKey("PalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pallet");
                });

            modelBuilder.Entity("Models.Pallet", b =>
                {
                    b.Navigation("Boxes");
                });
#pragma warning restore 612, 618
        }
    }
}
