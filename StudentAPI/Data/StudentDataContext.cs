using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data.Configuration;
using StudentAPI.Entities;
using StudentAPI.Entities.IdentityEntities;
using System.Reflection;

namespace StudentAPI.Data
{
    public class StudentDataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public StudentDataContext(DbContextOptions<StudentDataContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<LessonStudent> LessonStudent { get; set; }
        public DbSet<UniversityTracker> UniversityTrackers { get; set; }
    }
}
