namespace StudentAPI.Entities
{
    public class Schedule : BaseModel<Guid>
    {
        public DateTime Date { get; set; }

        public ICollection<ScheduleCourse> ScheduleCourses { get; set; }
    }
}
