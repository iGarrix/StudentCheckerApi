using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Dto;
using StudentAPI.Entities;
using StudentAPI.Entities.IdentityEntities;
using StudentAPI.Helper;

namespace StudentAPI.Repositories
{
    public class LessonRepository
    {
        private readonly StudentDataContext _db;
        private readonly UserManager<Person> _persons;
        private readonly IMapper _mapper;

        public LessonRepository(StudentDataContext db, IMapper mapper, UserManager<Person> persons)
        {
            _db = db;
            _mapper = mapper;
            _persons = persons;
        }

        public IEnumerable<LessonResponse> GetAllLessons()
        {
            var result = new List<LessonResponse>();
            foreach (var students in _db.Students.Include(i => i.Group).ToList())
            {
                result.Add(new LessonResponse() {
                    StudentLessonDto = _db.StudentLessons.Include(i => i.Student).Include(i => i.Lesson).FirstOrDefault(w => w.Student.Id == students.Id).ToDto<StudentLesson, StudentLessonDto>(_mapper),
                    GroupDto = students.Group.ToDto<Group, GroupDto>(_mapper),
                });
            }
            return result;
        }
    }
}
