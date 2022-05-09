using Microsoft.AspNetCore.Identity;

namespace StudentAPI.Entities.IdentityEntities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime Create { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TimePass { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }  
    }
}
