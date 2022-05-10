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
    public class ScheduleRepository
    {
        private readonly StudentDataContext _db;
        private readonly UserManager<Person> _persons;
        private readonly IMapper _mapper;

        public ScheduleRepository(StudentDataContext db, IMapper mapper, UserManager<Person> persons)
        {
            _db = db;
            _mapper = mapper;
            _persons = persons;
        }

        public IEnumerable<ScheduleResponse> GetSchedule()
        {
            //var dbfind = _db.ScheduleCourses.Include(i => i.Schedule).Include(i => i.Course).ToList();
            //var result = new List<ScheduleResponse>();
            //foreach (var item in _db.Schedules.ToList())
            //{
            //    var tempList = new List<LessonDto>();
            //    foreach (var it in dbfind.Select(s => s.Course))
            //    {
            //        foreach (var it2 in _db.Lessons.Include(i => i.Course).Where(w => w.Course.Id == it.Id))
            //        {
            //            tempList.Add(it2.ToDto<Lesson, LessonDto>(_mapper));
            //        }
            //    }
            //    result.Add(new ScheduleResponse()
            //    {
            //        ScheduleDto = item.ToDto<Schedule, ScheduleDto>(_mapper),
            //        LessonDtos = tempList,
            //    });
            //}
            //return result;

            var result = new List<ScheduleResponse>();
            foreach (var item in _db.Schedules.ToList())
            {
                var tempList = new List<LessonDto>();
                foreach (var item1 in _db.ScheduleCourses.Include(i => i.Schedule).Include(i => i.Course).ThenInclude(i => i.Lessons).Where(w => w.Schedule.Id == item.Id).Select(s => s.Course).Select(s => s.Lessons))
                {
                    tempList.AddRange(item1.ToList().ToListDto<Lesson, LessonDto>(_mapper));
                }
                result.Add(new ScheduleResponse()
                {
                    ScheduleDto = item.ToDto<Schedule, ScheduleDto>(_mapper),
                    LessonDtos = tempList,
                });
            }

            return result;
        }
    }
}
