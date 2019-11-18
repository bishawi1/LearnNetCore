﻿// <auto-generated />
using System;
using MSIS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MSIS.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20191115191908_Offers")]
    partial class Offers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MSIS.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("EmployeeId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MSIS.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("MSIS.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrencyCode");

                    b.Property<string>("CurrencyName");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("MSIS.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("OtherInformation");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MSIS.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int?>("Department");

                    b.Property<string>("EducationDegree");

                    b.Property<string>("Email");

                    b.Property<string>("IdentityNo")
                        .IsRequired();

                    b.Property<string>("MobileNo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OtherInformation");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("Specialization");

                    b.Property<string>("WorkMobileNo");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MSIS.Models.ItemUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ItemUnitName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ItemUnits");
                });

            modelBuilder.Entity("MSIS.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Fax")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MobileNo")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("OtheInformation");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProjectOwner");

                    b.Property<int>("ProjectSerial");

                    b.Property<string>("ProjectYear")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 64)))
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("ProjectOwner");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MSIS.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrencyId");

                    b.Property<float>("CurrencyRate");

                    b.Property<string>("Notes");

                    b.Property<string>("PurchaseOrderCode")
                        .IsRequired();

                    b.Property<DateTime>("PurchaseOrderDate");

                    b.Property<int>("PurchaseOrderNo");

                    b.Property<int>("PurchaseOrderYear");

                    b.Property<int>("SupplierId");

                    b.Property<DateTime>("Time_Stamp");

                    b.Property<string>("User_Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("MSIS.Models.PurchaseOrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ItemName")
                        .IsRequired();

                    b.Property<int>("ItemUnitId");

                    b.Property<int>("PuchaseOrderId");

                    b.Property<float>("QNT");

                    b.Property<float>("UnitPrice");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrdersDetails");
                });

            modelBuilder.Entity("MSIS.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("MobileNo");

                    b.Property<string>("OtherInformation");

                    b.Property<string>("Phone");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("MSIS.Models.TaskAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActionDate");

                    b.Property<int>("CurrentTaskStatusId");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId");

                    b.Property<int>("TaskStatusId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TaskActions");
                });

            modelBuilder.Entity("MSIS.Models.TaskStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("StatusName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("TaskStatus");
                });

            modelBuilder.Entity("MSIS.Models.TaskTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId");

                    b.Property<int>("TaskId");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TaskTeams");
                });

            modelBuilder.Entity("MSIS.Models.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(Max)");

                    b.Property<string>("OtherInformation")
                        .HasColumnType("nvarchar(Max)");

                    b.Property<int>("ProjectId");

                    b.Property<DateTime>("TaskDate")
                        .HasColumnType("Date");

                    b.Property<DateTime>("TaskEndDate")
                        .HasColumnType("Date");

                    b.Property<int>("TaskOwnerId");

                    b.Property<int>("TaskResponsibleId");

                    b.Property<string>("TaskResultDescription")
                        .HasColumnType("nvarchar(Max)");

                    b.Property<DateTime>("TaskStartDate")
                        .HasColumnType("Date");

                    b.Property<int>("TaskStatusId");

                    b.Property<string>("TaskSubject")
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("Time_Stamp");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("MSIS.ViewModels.ListPurchaseOrderDetailsViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CurrencyCode");

                    b.Property<int>("CurrencyId");

                    b.Property<string>("CurrencyName");

                    b.Property<float>("CurrencyRate");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("MobileNo");

                    b.Property<string>("Notes");

                    b.Property<string>("Phone");

                    b.Property<string>("PurchaseOrderCode");

                    b.Property<DateTime>("PurchaseOrderDate");

                    b.Property<int>("PurchaseOrderNo");

                    b.Property<int>("PurchaseOrderYear");

                    b.Property<int>("SupplierId");

                    b.Property<string>("SupplierName");

                    b.Property<DateTime>("Time_Stamp");

                    b.Property<double>("TotalPrice");

                    b.Property<string>("User_Name");

                    b.HasKey("Id");

                    b.ToTable("SQLListPurchaseOrderDetailsViewModel");
                });

            modelBuilder.Entity("MSIS.ViewModels.ProjectDetailViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Code");

                    b.Property<string>("CustomerName");

                    b.Property<string>("Email");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("MobileNo");

                    b.Property<string>("OtheInformation");

                    b.Property<string>("ProjectName");

                    b.Property<int?>("ProjectOwner");

                    b.Property<int?>("ProjectSerial");

                    b.Property<int?>("ProjectYear");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("ProjectDetails");
                });

            modelBuilder.Entity("MSIS.ViewModels.PurchaseOrderDetailsViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CurrencyCode");

                    b.Property<int>("CurrencyId");

                    b.Property<string>("CurrencyName");

                    b.Property<float>("CurrencyRate");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("MobileNo");

                    b.Property<string>("Notes");

                    b.Property<string>("Phone");

                    b.Property<string>("PurchaseOrderCode");

                    b.Property<DateTime>("PurchaseOrderDate");

                    b.Property<int>("PurchaseOrderNo");

                    b.Property<int>("PurchaseOrderYear");

                    b.Property<int>("SupplierId");

                    b.Property<string>("SupplierName");

                    b.Property<DateTime>("Time_Stamp");

                    b.Property<double>("TotalPrice");

                    b.Property<string>("User_Name");

                    b.HasKey("Id");

                    b.ToTable("SQLPurchaseOrderDetailsViewModel");
                });

            modelBuilder.Entity("MSIS.ViewModels.PurchaseOrderItemsViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ItemName");

                    b.Property<int>("ItemUnitId");

                    b.Property<string>("ItemUnitName");

                    b.Property<int>("PuchaseOrderId");

                    b.Property<float>("QNT");

                    b.Property<float>("TotalPrice");

                    b.Property<float>("UnitPrice");

                    b.HasKey("Id");

                    b.ToTable("SQLPurchaseOrderItemsViewModel");
                });

            modelBuilder.Entity("MSIS.ViewModels.TaskDetailsViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BranchCode");

                    b.Property<int>("BranchId");

                    b.Property<string>("BranchName");

                    b.Property<string>("Description");

                    b.Property<string>("OtherInformation");

                    b.Property<int>("ProjectId");

                    b.Property<string>("ProjectName");

                    b.Property<int>("ProjectSerial");

                    b.Property<string>("ProjectYear");

                    b.Property<string>("StatusName");

                    b.Property<string>("TaskActionDetails");

                    b.Property<DateTime>("TaskDate");

                    b.Property<DateTime>("TaskEndDate");

                    b.Property<string>("TaskOperation");

                    b.Property<int>("TaskOwnerId");

                    b.Property<string>("TaskOwnerName");

                    b.Property<string>("TaskOwnerUserName");

                    b.Property<int>("TaskResponsibleId");

                    b.Property<string>("TaskResponsibleName");

                    b.Property<string>("TaskResultDescription");

                    b.Property<DateTime>("TaskStartDate");

                    b.Property<string>("TaskStatusCode");

                    b.Property<int>("TaskStatusId");

                    b.Property<string>("TaskSubject");

                    b.Property<DateTime>("Time_Stamp");

                    b.Property<string>("UserName");

                    b.Property<string>("strGroupBy");

                    b.HasKey("Id");

                    b.ToTable("SQLTaskDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MSIS.Models.Project", b =>
                {
                    b.HasOne("MSIS.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("ProjectOwner")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MSIS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MSIS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MSIS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MSIS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
