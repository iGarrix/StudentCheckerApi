namespace StudentAPI.Dto
{
    public class UniversityDto
    {
        public Guid Id { get; set; }
        public bool Visit { get; set; }
        public StudentDto StudentDto { get; set; }
    }
}
