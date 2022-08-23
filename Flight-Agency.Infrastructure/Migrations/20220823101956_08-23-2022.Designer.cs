﻿// <auto-generated />
using System;
using FlightAgency.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlightAgency.Infrastructure.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20220823101956_08-23-2022")]
    partial class _08232022
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.4.22229.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlightAgency.Models.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TripId")
                        .HasColumnType("integer");

                    b.Property<int?>("TripId1")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.HasIndex("TripId1");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("FlightAgency.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("FlightAgency.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlightAgency.Models.Stop", b =>
                {
                    b.HasOne("FlightAgency.Models.Trip", null)
                        .WithMany("SortedStops")
                        .HasForeignKey("TripId");

                    b.HasOne("FlightAgency.Models.Trip", null)
                        .WithMany("Stops")
                        .HasForeignKey("TripId1");

                    b.OwnsOne("FlightAgency.Models.DateRange", "Time", b1 =>
                        {
                            b1.Property<int>("StopId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("StopId");

                            b1.ToTable("Dates");

                            b1.WithOwner()
                                .HasForeignKey("StopId");
                        });

                    b.OwnsOne("FlightAgency.Models.Location", "Location", b1 =>
                        {
                            b1.Property<int>("StopId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .HasColumnType("integer");

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision");

                            b1.HasKey("StopId");

                            b1.ToTable("Locations");

                            b1.WithOwner()
                                .HasForeignKey("StopId");
                        });

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("Time")
                        .IsRequired();
                });

            modelBuilder.Entity("FlightAgency.Models.Trip", b =>
                {
                    b.HasOne("FlightAgency.Models.User", null)
                        .WithMany("Trips")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FlightAgency.Models.Trip", b =>
                {
                    b.Navigation("SortedStops");

                    b.Navigation("Stops");
                });

            modelBuilder.Entity("FlightAgency.Models.User", b =>
                {
                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
