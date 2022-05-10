using StudentAPI.Dto;

namespace StudentAPI.Helper
{
    public class ScheduleResponse
    {
        public ScheduleDto ScheduleDto { get; set; }
        public IEnumerable<LessonDto> LessonDtos { get; set; }
    }
}
