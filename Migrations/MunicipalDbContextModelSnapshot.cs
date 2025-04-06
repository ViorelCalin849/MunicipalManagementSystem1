﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MunicipalManagementSystem.Data;

#nullable disable

namespace MunicipalManagementSystem.Migrations
{
    [DbContext(typeof(MunicipalDbContext))]
    partial class MunicipalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MunicipalManagementSystem.Models.Citizen", b =>
                {
                    b.Property<int>("CitizenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CitizenID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CitizenID");

                    b.ToTable("Citizens");
                });

            modelBuilder.Entity("MunicipalManagementSystem.Models.Report", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportID"));

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StaffID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReportID");

                    b.HasIndex("CitizenID");

                    b.HasIndex("StaffID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("MunicipalManagementSystem.Models.ServiceRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<int>("CitizenID")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StaffID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestID");

                    b.HasIndex("CitizenID");

                    b.HasIndex("StaffID");

                    b.ToTable("ServiceRequests");
                });

            modelBuilder.Entity("MunicipalManagementSystem.Models.Staff", b =>
                {
                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffID"));

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("MunicipalManagementSystem.Models.Report", b =>
                {
                    b.HasOne("MunicipalManagementSystem.Models.Citizen", "Citizen")
                        .WithMany("Reports")
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MunicipalManagementSystem.Models.Staff", "Staff")
                        .WithMany("Reports")
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("MunicipalManagementSystem.Models.ServiceRequest", b =>
                {
                    b.HasOne("MunicipalManagementSystem.Models.Citizen", "Citizen")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MunicipalManagementSystem.Models.Staff", "Staff")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("MunicipalManagementSystem.Models.Citizen", b =>
                {
                    b.Navigation("Reports");

                    b.Navigation("ServiceRequests");
                });

            modelBuilder.Entity("MunicipalManagementSystem.Models.Staff", b =>
                {
                    b.Navigation("Reports");

                    b.Navigation("ServiceRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
