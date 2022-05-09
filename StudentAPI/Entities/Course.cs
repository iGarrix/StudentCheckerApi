using StudentAPI.Entities.IdentityEntities;

namespace StudentAPI.Entities
{
    public class Course : BaseModel<Guid>
    {
        public string Name { get; set; }
    }
}
