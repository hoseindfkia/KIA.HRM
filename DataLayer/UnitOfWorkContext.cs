using DomainClass;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainClass.UserEntity;

namespace DataLayer
{
    public class UnitOfWorkContext : DbContext, IUnitOfWorkContext
    {

        public UnitOfWorkContext(DbContextOptions<UnitOfWorkContext> options)
            : base(options) { }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }


        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<ProjectActionEntity> ProjectActions { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<DegreeTypeEntity> DegreeTypes { get; set; }
        public DbSet<RoleAccessEntity> RoleAccesss { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<ProjectActionAssignUserEntity> ProjectActionAssignUsers { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<ProvinceEntity> Provinces{ get; set; }
        public DbSet<CalendarDayEntity> CalendarDays { get; set; }




        #region WorkReport

        public DbSet<PreparationDocumentEntity> PreparationDocuments { get; set; }
        public DbSet<MeetingEntity> Meetings { get; set; }
        public DbSet<MissionEntity> Missions { get; set; }
        public DbSet<LeaveEntity> Leaves { get; set; }
        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table names
            modelBuilder.Entity<UserEntity>().ToTable("User");
            modelBuilder.Entity<ProjectEntity>().ToTable("Project");
            modelBuilder.Entity<RoleEntity>().ToTable("Role");
            modelBuilder.Entity<ProjectActionEntity>().ToTable("ProjectAction");
            modelBuilder.Entity<UserRoleEntity>().ToTable("UserRole");
            modelBuilder.Entity<DegreeTypeEntity>().ToTable("DegreeType");
            modelBuilder.Entity<RoleAccessEntity>().ToTable("RoleAccess");
            modelBuilder.Entity<FileEntity>().ToTable("File");
            modelBuilder.Entity<CityEntity>().ToTable("City");
            modelBuilder.Entity<ProvinceEntity>().ToTable("Province");
            modelBuilder.Entity<CalendarDayEntity>().ToTable("CalendarDay");


            modelBuilder.Entity<PreparationDocumentEntity>().ToTable("PreparationDocument");
            modelBuilder.Entity<MeetingEntity>().ToTable("Meeting");
            modelBuilder.Entity<MissionEntity>().ToTable("Mission");
            modelBuilder.Entity<LeaveEntity>().ToTable("Leave");
            #endregion

            #region Config

            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.ProjectActionOrigins)
                .WithOne(x => x.UserOrigin)
                .HasForeignKey(x => x.UserOriginId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.ProjectActionAssignUsers)
                .WithOne(x => x.UserAssigned)
                .HasForeignKey(x => x.UserAssignedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserEntity>()
               .HasMany(x => x.UserRoles)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<UserEntity>()
            //    .HasMany(x=> x.ProjectActionDestinations)
            //    .WithOne(x => x.UserDestination)
            //    .HasForeignKey(x => x.UserDestinationId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion


        }


    }
}
