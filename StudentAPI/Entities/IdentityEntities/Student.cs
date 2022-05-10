using Microsoft.AspNetCore.Identity;

namespace StudentAPI.Entities.IdentityEntities
{
    public class Student : Person
    {
        public int TimePass { get; set; }


        public ICollection<StudentLesson> StudentLessons { get; set; }
        public ICollection<UniversityTracker> UniversityTrackers { get; set; }

    }
}
