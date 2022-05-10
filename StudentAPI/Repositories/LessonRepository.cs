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

        public IEnumerable<LessonResponse> GetStudentLesson(string Id)
        {
            //var result = new List<LessonResponse>();
            //foreach (var students in _db.StudentLessons.Include(i => i.Student).ThenInclude(i => i.Group).Include(i => i.Lesson).Where(w => w.Lesson.Id == Guid.Parse(Id)).Select(s => s.Student).ToList())
            //{
            //    result.Add(new LessonResponse()
            //    {
            //        StudentLessonDto = _db.StudentLessons.Include(i => i.Student).Include(i => i.Lesson).FirstOrDefault(w => w.Student.Id == students.Id).ToDto<StudentLesson, StudentLessonDto>(_mapper),
            //        GroupDto = students.Group.ToDto<Group, GroupDto>(_mapper),
            //    });
            //}
            //return result;

            var result = new List<LessonResponse>();
            foreach (var stlesson in _db.StudentLessons.Include(i => i.Student).ThenInclude(i => i.Group).Include(i => i.Lesson).Where(w => w.Lesson.Id == Guid.Parse(Id)).ToList())
            {
                result.Add(new LessonResponse()
                {
                    StudentLessonDto = _db.StudentLessons.Include(i => i.Student).Include(i => i.Lesson).FirstOrDefault(w => w.Id == stlesson.Id).ToDto<StudentLesson, StudentLessonDto>(_mapper),
                    GroupDto = stlesson.Student.Group.ToDto<Group, GroupDto>(_mapper),
                });
            }
            return result;
        }

        public async Task<string> LessonChange(string Id, string email, bool lessonVisit)
        {
            var findLesson = _db.StudentLessons.FirstOrDefault(f => f.Id == Guid.Parse(Id));
            if (findLesson == null)
            {
                throw new Exception("Lesson not found");
            }

            var findStudent = (await _persons.FindByEmailAsync(email)) as Student;
            if (findStudent is null)
            {
                throw new Exception("Student not found");
            }

            var unv = _db.UniversityTrackers.Include(i => i.Student).FirstOrDefault(f => f.Student.Id == findStudent.Id);
            if (unv is not null)
            {
                if (unv.Visit == true)
                {
                    findLesson.LessonVisit = lessonVisit;
                    _db.StudentLessons.Update(findLesson);
                    _db.SaveChanges();
                    return "Change successfully";
                }
                findStudent.TimePass += 20;
                var result = await _persons.UpdateAsync(findStudent);
                if (!result.Succeeded)
                {
                    throw new Exception("Adding timepass failed");
                }
                return "Adding timepass success";
            }
            throw new Exception("University checker not found");
        }
    }
}
