using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Entities.IdentityEntities
{
    public abstract class Person : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
    }
}
