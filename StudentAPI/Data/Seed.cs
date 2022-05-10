using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Entities;
using StudentAPI.Entities.IdentityEntities;
using StudentAPI.Helper;

namespace StudentAPI.Data
{
    public static class Seed
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<StudentDataContext>();
                    context.Database.Migrate();
                    Init(scope);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }

        private static void Init(IServiceScope scope)
        {
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<StudentDataContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var dbPerson = scope.ServiceProvider.GetRequiredService<UserManager<Person>>();
                if (!roleManager.Roles.Any())
                {
                    var result = roleManager.CreateAsync(new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "User"
                    }).Result;
                }
                if (!db.Courses.Any())
                {
                    db.Courses.Add(new Course()
                    {
                        Create = DateTime.Now,
                        Name = "Math"
                    });
                    db.Courses.Add(new Course()
                    {
                        Create = DateTime.Now,
                        Name = "Other languages"
                    });
                    db.Courses.Add(new Course()
                    {
                        Create = DateTime.Now,
                        Name = "IT"
                    });

                    db.SaveChanges();
                }
                if (!db.Groups.Any())
                {
                    db.Groups.Add(new Group()
                    {
                        Create = DateTime.Now,
                        Name = "PD1"
                    });
                    db.SaveChanges();
                }
                if (!dbPerson.Users.Any())
                {
                    var bob = new Student()
                    {
                        Id = Guid.NewGuid(),
                        Create = DateTime.Now,
                        UserName = "Bob",
                        Name = "Bob",
                        Surname = "Bobov",
                        Email = "bob@gmail.com",
                        PhoneNumber = "1534567890",
                        TimePass = 5,
                        Group = db.Groups.FirstOrDefault(f => f.Name == "PD1"),
                    };
                    var ernie = new Student()
                    {
                        Id = Guid.NewGuid(),
                        Create = DateTime.Now,
                        UserName = "Ernie",
                        Name = "Ernie",
                        Surname = "Tester",
                        Email = "ernie@gmail.com",
                        PhoneNumber = "1234567891",
                        TimePass = 8,
                        Group = db.Groups.FirstOrDefault(f => f.Name == "PD1"),
                    };
                    var resultBob = dbPerson.CreateAsync(bob, "1234").Result;
                    var resultErnie = dbPerson.CreateAsync(ernie, "1234").Result;

                    if (resultBob.Succeeded)
                    {
                        resultBob = dbPerson.AddToRoleAsync(bob, "User").Result;
                    }
                    if (resultErnie.Succeeded)
                    {
                        resultErnie = dbPerson.AddToRoleAsync(ernie, "User").Result;
                    }

                    var Alex = new Teacher()
                    {
                        Id = Guid.NewGuid(),
                        Create = DateTime.Now,
                        UserName = "Alex",
                        Name = "Alex",
                        Surname = "Alexiev",
                        Email = "alexe@gmail.com",
                        PhoneNumber = "1234577891",
                    };
                    var William = new Teacher()
                    {
                        Id = Guid.NewGuid(),
                        Create = DateTime.Now,
                        UserName = "William",
                        Name = "William",
                        Surname = "Williamser",
                        Email = "william@gmail.com",
                        PhoneNumber = "1239567891",
                    };
                    var resultAlex = dbPerson.CreateAsync(Alex, "1234").Result;
                    var resultWilliam = dbPerson.CreateAsync(William, "1234").Result;

                    if (resultAlex.Succeeded)
                    {
                        resultAlex = dbPerson.AddToRoleAsync(Alex, "User").Result;
                    }
                    if (resultWilliam.Succeeded)
                    {
                        resultWilliam = dbPerson.AddToRoleAsync(William, "User").Result;
                    }
                }
                if (!db.UniversityTrackers.Any())
                {
                    db.UniversityTrackers.Add(new UniversityTracker() { 
                        Create = DateTime.Now,
                        Visit = false,
                        Student = dbPerson.FindByEmailAsync("bob@gmail.com").Result as Student,
                    });
                    db.UniversityTrackers.Add(new UniversityTracker()
                    {
                        Create = DateTime.Now,
                        Visit = false,
                        Student = dbPerson.FindByEmailAsync("ernie@gmail.com").Result as Student,
                    });
                    db.SaveChanges();
                }
                if (!db.Lessons.Any())
                {
                    db.Lessons.Add(new Lesson()
                    {
                        Name = "Base math",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Math"),           
                    });
                    db.Lessons.Add(new Lesson()
                    {
                        Name = "Middle math",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Math"),
                    });
                    db.Lessons.Add(new Lesson()
                    {
                        Name = "High math",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Math"),
                    });

                    db.Lessons.Add(new Lesson()
                    {
                        Name = "Other 1",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Other languages"),
                    });
                    db.Lessons.Add(new Lesson()
                    {
                        Name = "Other 2",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Other languages"),
                    });
                    db.Lessons.Add(new Lesson()
                    {
                        Name = "Other 3",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Other languages"),
                    });

                    db.Lessons.Add(new Lesson()
                    {
                        Name = "CSHARP",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "IT"),
                    });
                    db.Lessons.Add(new Lesson()
                    {
                        Name = "C++",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "IT"),
                    });
                    db.Lessons.Add(new Lesson()
                    {
                        Name = "REACT",
                        StartTime = DateTime.Now,
                        Create = DateTime.Now,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "IT"),
                    });

                    db.SaveChanges();
                }
                if (!db.Schedules.Any())
                {
                    db.Schedules.Add(new Schedule()
                    {
                        Create = DateTime.Now,
                        Date = new DateTime(2022, 5, 10),              
                    });
                    db.Schedules.Add(new Schedule()
                    {
                        Create = DateTime.Now,
                        Date = new DateTime(2022, 5, 12),
                    });
                    db.Schedules.Add(new Schedule()
                    {
                        Create = DateTime.Now,
                        Date = new DateTime(2022, 5, 14),
                    });
                    db.Schedules.Add(new Schedule()
                    {
                        Create = DateTime.Now,
                        Date = new DateTime(2022, 5, 16),
                    });

                    db.SaveChanges();
                }
                if (!db.ScheduleCourses.Any())
                {
                    db.ScheduleCourses.Add(new ScheduleCourse()
                    {
                        Create = DateTime.Now,
                        Teacher = dbPerson.FindByEmailAsync("alexe@gmail.com").Result as Teacher,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Math"),
                        Schedule = db.Schedules.FirstOrDefault(f => f.Date.Day == 10),
                    });
                    db.ScheduleCourses.Add(new ScheduleCourse()
                    {
                        Create = DateTime.Now,
                        Teacher = dbPerson.FindByEmailAsync("alexe@gmail.com").Result as Teacher,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "IT"),
                        Schedule = db.Schedules.FirstOrDefault(f => f.Date.Day == 10),
                    });

                    db.ScheduleCourses.Add(new ScheduleCourse()
                    {
                        Create = DateTime.Now,
                        Teacher = dbPerson.FindByEmailAsync("william@gmail.com").Result as Teacher,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Math"),
                        Schedule = db.Schedules.FirstOrDefault(f => f.Date.Day == 12),
                    });
                    db.ScheduleCourses.Add(new ScheduleCourse()
                    {
                        Create = DateTime.Now,
                        Teacher = dbPerson.FindByEmailAsync("william@gmail.com").Result as Teacher,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "IT"),
                        Schedule = db.Schedules.FirstOrDefault(f => f.Date.Day == 12),
                    });
                    db.ScheduleCourses.Add(new ScheduleCourse()
                    {
                        Create = DateTime.Now,
                        Teacher = dbPerson.FindByEmailAsync("william@gmail.com").Result as Teacher,
                        Course = db.Courses.FirstOrDefault(f => f.Name == "Math"),
                        Schedule = db.Schedules.FirstOrDefault(f => f.Date.Day == 12),
                    });
                    db.SaveChanges();
                }
                if (!db.StudentLessons.Any())
                {
                    foreach (var item in db.Lessons.ToList())
                    {
                        db.StudentLessons.Add(new StudentLesson()
                        {
                            Create = DateTime.Now,
                            LessonVisit = false,
                            Student = dbPerson.FindByEmailAsync("bob@gmail.com").Result as Student,
                            Lesson = db.Lessons.FirstOrDefault(f => f.Name == item.Name),
                        });
                        db.StudentLessons.Add(new StudentLesson()
                        {
                            Create = DateTime.Now,
                            LessonVisit = false,
                            Student = dbPerson.FindByEmailAsync("ernie@gmail.com").Result as Student,
                            Lesson = db.Lessons.FirstOrDefault(f => f.Name == item.Name),
                        });
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
