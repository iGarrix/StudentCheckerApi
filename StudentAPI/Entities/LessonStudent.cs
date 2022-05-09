using StudentAPI.Entities.IdentityEntities;

namespace StudentAPI.Entities
{
    public class LessonStudent : BaseModel<Guid>
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        public bool LessonVisit { get; set; }

    }
}
