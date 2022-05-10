namespace StudentAPI.Entities
{
    public class Lesson : BaseModel<Guid>
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<StudentLesson> StudentLessons { get; set; }
    }
}
