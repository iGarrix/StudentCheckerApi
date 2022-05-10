using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Entities;
using StudentAPI.Entities.IdentityEntities;
using System.Reflection;

namespace StudentAPI.Data
{
    public class StudentDataContext : IdentityDbContext<Person, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
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

            builder.Entity<Lesson>()
                .HasOne(o => o.Course).WithMany(m => m.Lessons).HasForeignKey(o => o.CourseId);

            builder.Entity<Student>()
                .HasOne(o => o.Group).WithMany(m => m.Students).HasForeignKey(o => o.GroupId);

            builder.Entity<UniversityTracker>()
               .HasOne(o => o.Student).WithMany(m => m.UniversityTrackers).HasForeignKey(o => o.StudentId);

            builder.Entity<ScheduleCourse>()
                .HasOne(o => o.Course).WithMany(m => m.ScheduleCourses).HasForeignKey(o => o.CourseId);

            builder.Entity<ScheduleCourse>()
               .HasOne(o => o.Schedule).WithMany(m => m.ScheduleCourses).HasForeignKey(o => o.ScheduleId);

            builder.Entity<ScheduleCourse>()
               .HasOne(o => o.Teacher).WithMany(m => m.ScheduleCourses).HasForeignKey(o => o.TeacherId);

            builder.Entity<StudentLesson>()
               .HasOne(o => o.Student).WithMany(m => m.StudentLessons).HasForeignKey(o => o.StudentId);

            builder.Entity<StudentLesson>()
               .HasOne(o => o.Lesson).WithMany(m => m.StudentLessons).HasForeignKey(o => o.LessonId);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ScheduleCourse> ScheduleCourses { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<UniversityTracker> UniversityTrackers { get; set; }
    }
}
