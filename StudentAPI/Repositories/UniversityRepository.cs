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
    public class UniversityRepository
    {
        private readonly StudentDataContext _db;
        private readonly UserManager<Person> _persons;
        private readonly IMapper _mapper;

        public UniversityRepository(StudentDataContext db, IMapper mapper, UserManager<Person> persons)
        {
            _db = db;
            _mapper = mapper;
            _persons = persons;
        }

        public IEnumerable<UniversityDto> GetTrackers()
        {
            return _db.UniversityTrackers.Include(f => f.Student).ToList().ToListDto<UniversityTracker, UniversityDto>(_mapper);
        }

        public async Task<UniversityDto> ChangeChecker(string email, bool visit)
        {
            var findUser = await _persons.FindByEmailAsync(email);
            if (findUser == null)
            {
                throw new Exception("Student not found");
            }

            var findTracker = _db.UniversityTrackers.Include(f => f.Student).FirstOrDefault(f => f.Student.Id == findUser.Id);
            findTracker.Visit = visit;
            _db.UniversityTrackers.Update(findTracker);
            _db.SaveChanges();
            return findTracker.ToDto<UniversityTracker, UniversityDto>(_mapper);
        }
    }
}
