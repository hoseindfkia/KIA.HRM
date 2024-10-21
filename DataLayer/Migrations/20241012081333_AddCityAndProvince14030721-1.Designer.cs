﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(UnitOfWorkContext))]
    [Migration("20241012081333_AddCityAndProvince14030721-1")]
    partial class AddCityAndProvince140307211
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DomainClass.CryptographyEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("IV")
                        .HasMaxLength(16)
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("Key")
                        .HasMaxLength(16)
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("CryptographyEntity");
                });

            modelBuilder.Entity("DomainClass.DegreeTypeEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("DegreeType", (string)null);
                });

            modelBuilder.Entity("DomainClass.FileEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("EncryptionKey")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<int>("FormType")
                        .HasColumnType("int");

                    b.Property<bool>("IsEncrypt")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("File", (string)null);
                });

            modelBuilder.Entity("DomainClass.ProjectActionAssignUserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProjectActionId")
                        .HasColumnType("bigint");

                    b.Property<int>("ProjectActionStatusType")
                        .HasColumnType("int");

                    b.Property<long>("UserAssignedId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserRoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectActionId");

                    b.HasIndex("UserAssignedId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("ProjectActionAssignUsers");
                });

            modelBuilder.Entity("DomainClass.ProjectActionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DegreeOtherDescription")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DegreeTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("DegreeTypeTitle")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectActionStatusType")
                        .HasColumnType("int");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<long>("UserOriginId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DegreeTypeId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserOriginId");

                    b.ToTable("ProjectAction", (string)null);
                });

            modelBuilder.Entity("DomainClass.ProjectEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<long>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatorId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("DomainClass.ProjectFileEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectActionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("ProjectActionId");

                    b.ToTable("ProjectFileEntity");
                });

            modelBuilder.Entity("DomainClass.RoleAccessEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessType")
                        .HasColumnType("int");

                    b.Property<int>("ActionType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("RoleAccess", (string)null);
                });

            modelBuilder.Entity("DomainClass.RoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("DomainClass.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<bool>("EmailConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTimeOffset>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NationalCode")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("DomainClass.UserRoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("DomainClass.WorkReport.LeaveEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ApproverUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int>("LeaveType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Leave", (string)null);
                });

            modelBuilder.Entity("DomainClass.WorkReport.MeetingEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ApproverUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Meeting", (string)null);
                });

            modelBuilder.Entity("DomainClass.WorkReport.MeetingFileEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long>("MeetingId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("MeetingId");

                    b.ToTable("MeetingFileEntity");
                });

            modelBuilder.Entity("DomainClass.WorkReport.MissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ApproverUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int>("MissionType")
                        .HasColumnType("int");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Mission", (string)null);
                });

            modelBuilder.Entity("DomainClass.WorkReport.MissionFileEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long>("MissionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("MissionId");

                    b.ToTable("MissionFileEntity");
                });

            modelBuilder.Entity("DomainClass.WorkReport.PreparationDocumentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ApproverUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DocumentId")
                        .HasColumnType("bigint");

                    b.Property<int>("DocumentVersion")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserCreatorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("PreparationDocument", (string)null);
                });

            modelBuilder.Entity("DomainClass.CryptographyEntity", b =>
                {
                    b.HasOne("DomainClass.FileEntity", "File")
                        .WithOne("Cryptography")
                        .HasForeignKey("DomainClass.CryptographyEntity", "FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("DomainClass.ProjectActionAssignUserEntity", b =>
                {
                    b.HasOne("DomainClass.ProjectActionEntity", "ProjectAction")
                        .WithMany("ProjectActionAssignUsers")
                        .HasForeignKey("ProjectActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.UserEntity", "UserAssigned")
                        .WithMany("ProjectActionAssignUsers")
                        .HasForeignKey("UserAssignedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DomainClass.UserRoleEntity", "UserRole")
                        .WithMany("ProjectActionAssignUsers")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectAction");

                    b.Navigation("UserAssigned");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("DomainClass.ProjectActionEntity", b =>
                {
                    b.HasOne("DomainClass.DegreeTypeEntity", "DegreeType")
                        .WithMany("ProjectActions")
                        .HasForeignKey("DegreeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.ProjectEntity", "Project")
                        .WithMany("ProjectActions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.UserEntity", "UserOrigin")
                        .WithMany("ProjectActionOrigins")
                        .HasForeignKey("UserOriginId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DegreeType");

                    b.Navigation("Project");

                    b.Navigation("UserOrigin");
                });

            modelBuilder.Entity("DomainClass.ProjectEntity", b =>
                {
                    b.HasOne("DomainClass.UserEntity", "UserCreator")
                        .WithMany("Projects")
                        .HasForeignKey("UserCreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCreator");
                });

            modelBuilder.Entity("DomainClass.ProjectFileEntity", b =>
                {
                    b.HasOne("DomainClass.FileEntity", "File")
                        .WithMany("ProjectFiles")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.ProjectActionEntity", "ProjectAction")
                        .WithMany("ProjectFiles")
                        .HasForeignKey("ProjectActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("ProjectAction");
                });

            modelBuilder.Entity("DomainClass.UserRoleEntity", b =>
                {
                    b.HasOne("DomainClass.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainClass.WorkReport.MeetingFileEntity", b =>
                {
                    b.HasOne("DomainClass.FileEntity", "File")
                        .WithMany("MeetingFiles")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.WorkReport.MeetingEntity", "Meeting")
                        .WithMany("MeetingFiles")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("DomainClass.WorkReport.MissionFileEntity", b =>
                {
                    b.HasOne("DomainClass.FileEntity", "File")
                        .WithMany("MissionFiles")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.WorkReport.MissionEntity", "Mission")
                        .WithMany("MissionFiles")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Mission");
                });

            modelBuilder.Entity("DomainClass.DegreeTypeEntity", b =>
                {
                    b.Navigation("ProjectActions");
                });

            modelBuilder.Entity("DomainClass.FileEntity", b =>
                {
                    b.Navigation("Cryptography");

                    b.Navigation("MeetingFiles");

                    b.Navigation("MissionFiles");

                    b.Navigation("ProjectFiles");
                });

            modelBuilder.Entity("DomainClass.ProjectActionEntity", b =>
                {
                    b.Navigation("ProjectActionAssignUsers");

                    b.Navigation("ProjectFiles");
                });

            modelBuilder.Entity("DomainClass.ProjectEntity", b =>
                {
                    b.Navigation("ProjectActions");
                });

            modelBuilder.Entity("DomainClass.RoleEntity", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DomainClass.UserEntity", b =>
                {
                    b.Navigation("ProjectActionAssignUsers");

                    b.Navigation("ProjectActionOrigins");

                    b.Navigation("Projects");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DomainClass.UserRoleEntity", b =>
                {
                    b.Navigation("ProjectActionAssignUsers");
                });

            modelBuilder.Entity("DomainClass.WorkReport.MeetingEntity", b =>
                {
                    b.Navigation("MeetingFiles");
                });

            modelBuilder.Entity("DomainClass.WorkReport.MissionEntity", b =>
                {
                    b.Navigation("MissionFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
