using StudentAPI.Entities.IdentityEntities;

namespace StudentAPI.Entities
{
    public class StudentLesson : BaseModel<Guid>
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public bool LessonVisit { get; set; }
    }
}
