namespace StudentAPI.Entities
{
    public class Course : BaseModel<Guid>
    {
        public string Name { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<ScheduleCourse> ScheduleCourses { get; set; }
    }
}
