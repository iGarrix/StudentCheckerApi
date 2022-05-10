using StudentAPI.Entities.IdentityEntities;

namespace StudentAPI.Entities
{
    public class ScheduleCourse : BaseModel<Guid>
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
