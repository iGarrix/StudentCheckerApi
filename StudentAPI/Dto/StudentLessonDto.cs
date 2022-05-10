namespace StudentAPI.Dto
{
    public class StudentLessonDto
    {
        public Guid Id { get; set; }
        public StudentDto StudentDto { get; set; }
        public LessonDto LessonDto { get; set; }
        public bool LessonVisit { get; set; }
    }
}
