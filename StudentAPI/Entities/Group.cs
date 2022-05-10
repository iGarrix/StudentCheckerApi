using StudentAPI.Entities.IdentityEntities;

namespace StudentAPI.Entities
{
    public class Group : BaseModel<Guid>
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
