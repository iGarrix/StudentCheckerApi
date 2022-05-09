namespace StudentAPI.Entities
{
    public class Lesson : BaseModel<Guid>
    {   
        public string Name { get; set; }

        public Guid ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }

        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
