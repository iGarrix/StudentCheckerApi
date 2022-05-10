using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentAPI.Entities.IdentityEntities;
using StudentAPI.Helper;

namespace StudentAPI.Repositories
{
    public class AuthRepository
    {
        private readonly UserManager<Person> _persons;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<Person> persons, IMapper mapper, IConfiguration configuration)
        {
            _persons = persons;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<AuthorizateResponse<TDto>> Login<TEntity, TDto>(string email, string password) where TEntity : Person
        {

            var findUser = await _persons.FindByEmailAsync(email);
            if (findUser is null)
            {
                throw new Exception("Account not found");
            }

            bool passwordValid = await _persons.CheckPasswordAsync(findUser, password);
            if (passwordValid)
            {
                  return JwtHelper.CreateToken<TEntity, TDto>(findUser as TEntity, false, _persons, _mapper, _configuration);
            }
            throw new Exception("Password is not valid");
        }

        public async Task<TDto> Get<TEntity, TDto>(string email) where TEntity : Person
        {
            var findUser = await _persons.FindByEmailAsync(email);
            if (findUser is null)
            {
                throw new Exception("Account not found");
            }

            return (findUser as TEntity).ToDto<TEntity, TDto>(_mapper);
        }
    }
}
