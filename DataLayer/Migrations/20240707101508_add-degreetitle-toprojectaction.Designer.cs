﻿// <auto-generated />
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
    [Migration("20240707101508_add-degreetitle-toprojectaction")]
    partial class adddegreetitletoprojectaction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DomainClass.DegreeTypeEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("DegreeType", (string)null);
                });

            modelBuilder.Entity("DomainClass.FileEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("FileName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

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

                    b.Property<string>("DegreeOtherDescription")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<long>("DegreeTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("DegreeTypeTitle")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("ProjectActionStatusType")
                        .HasColumnType("int");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("RoleAccess", (string)null);
                });

            modelBuilder.Entity("DomainClass.RoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("DomainClass.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("NationalCode")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Password")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Username")
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

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("RoleEntityUserRoleEntity", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("RolesId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("RoleEntityUserRoleEntity");
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
                    b.HasOne("DomainClass.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoleEntityUserRoleEntity", b =>
                {
                    b.HasOne("DomainClass.UserRoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainClass.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DomainClass.DegreeTypeEntity", b =>
                {
                    b.Navigation("ProjectActions");
                });

            modelBuilder.Entity("DomainClass.FileEntity", b =>
                {
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
#pragma warning restore 612, 618
        }
    }
}
