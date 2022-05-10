namespace StudentAPI.Entities.IdentityEntities
{
    public class Teacher : Person
    {

        public ICollection<ScheduleCourse> ScheduleCourses { get; set; }
    }
}
