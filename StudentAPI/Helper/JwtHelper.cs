using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentAPI.Dto;
using StudentAPI.Entities;
using StudentAPI.Entities.IdentityEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentAPI.Helper
{
    public class JwtHelper
    {
        public static AuthorizateResponse<TDto> CreateToken<TEntity, TDto>(TEntity user, bool isRemember, UserManager<Person> userManager, IMapper mapper, IConfiguration configuration) where TEntity : Person
        {
            var roles = userManager.GetRolesAsync(user).Result;
            List<Claim> claims = new List<Claim>()
            {
                new Claim("email", user.Email),
            };
            if (roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim("roles", role));
                }
            }
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<String>("JwtKey")));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: isRemember ? DateTime.Now.AddDays(7) : DateTime.Now.AddDays(1),
                claims: claims
            );
            return new AuthorizateResponse<TDto>
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpiredIn = isRemember ? DateTime.Now.AddDays(7) : DateTime.Now.AddDays(1),
                Entity = user.ToDto<TEntity, TDto>(mapper),
            };
        }
    }

    public static class GenericExtension
    {
        public static TDest ToDto<TSource, TDest>(this TSource tSourse, IMapper mapper)
        {
            return mapper.Map<TSource, TDest>(tSourse);
        }
        public static IEnumerable<TDest> ToListDto<TSource, TDest>(this List<TSource> tSourses, IMapper mapper)
        {
            foreach (var item in tSourses)
            {
                yield return mapper.Map<TSource, TDest>(item);
            }
        }
    }

    public class AuthorizateResponse<TDto>
    {
        public string Token { get; set; }
        public DateTime ExpiredIn { get; set; }
        public TDto Entity { get; set; }
    }

    

    

    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>()
               .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
               .ForMember(dest => dest.Surname, source => source.MapFrom(src => src.Surname))
               .ForMember(dest => dest.Email, source => source.MapFrom(src => src.Email))
               .ForMember(dest => dest.TimePass, source => source.MapFrom(src => src.TimePass));
        }
    }

    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDto>()
               .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
               .ForMember(dest => dest.Surname, source => source.MapFrom(src => src.Surname))
               .ForMember(dest => dest.Email, source => source.MapFrom(src => src.Email));
        }
    }

    public class UniversityProfile : Profile
    {
        public UniversityProfile()
        {
            CreateMap<UniversityTracker, UniversityDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
               .ForMember(dest => dest.Visit, source => source.MapFrom(src => src.Visit))
               .ForMember(dest => dest.StudentDto, source => source.MapFrom(src => src.Student));
        }
    }

    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
               .ForMember(dest => dest.Date, source => source.MapFrom(src => src.Date));
        }
    }

    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, LessonDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
               .ForMember(dest => dest.StartTime, source => source.MapFrom(src => src.StartTime));
        }
    }

    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name));
        }
    }

    public class StudentLessonProfile : Profile
    {
        public StudentLessonProfile()
        {
            CreateMap<StudentLesson, StudentLessonDto>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                .ForMember(dest => dest.LessonVisit, source => source.MapFrom(src => src.LessonVisit))
                .ForMember(dest => dest.StudentDto, source => source.MapFrom(src => src.Student))
                .ForMember(dest => dest.LessonDto, source => source.MapFrom(src => src.Lesson));
        }
    }
}
