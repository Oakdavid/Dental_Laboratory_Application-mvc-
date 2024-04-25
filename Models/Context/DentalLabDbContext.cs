using Dental_lab_Application_MVC_.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace Dental_lab_Application_MVC_.Models.Context
{
    public class DentalLabDbContext : DbContext
    {
        public DentalLabDbContext(DbContextOptions<DentalLabDbContext> options
            ) : base( options )
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DentalService> DentalServices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DentalService>().Property(p => p.Cost).HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("6b541cc9-b08b-47d3-b52f-3ca6aa06a1e6"),
                Name = "HeadDoctor",
                
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("0504ea46-35aa-4949-9c59-b8b32a083ef6"),
                Name = "Doctor",
                
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("cf58ec7c-7829-4549-a924-ffe60ea3ca14"),
                Name = "Patient",
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("419460cf-bda5-41bd-a342-eb30c795fda3"),
                RoleId = new Guid("6b541cc9-b08b-47d3-b52f-3ca6aa06a1e6"),
                UserEmail = "headdoctor@gmail.com",
                UserName = "headdoctor",
                Password = "headdoctor",
            });

            modelBuilder.Entity<Profile>().HasData(new Profile
            {
                FirstName = "",
                LastName = "",
                Address = "",
                Id = new Guid("50b815eb-02e3-4579-9a83-1d39884ee56c"),
                Contact = "07031054058",
                Bio = "Easy going",
                Interests = "Scatting, playing chess and cooking",
                Skills = "Leather craft, Decoration",
                ProfilePicture = "",
                SocialMediaLink = "",
                UserId = new Guid("419460cf-bda5-41bd-a342-eb30c795fda3"),
                IsDeleted = false,
                
            });
        }


    }
}
