using StudentAPI.Entities.IdentityEntities;

namespace StudentAPI.Entities
{
    public class UniversityTracker : BaseModel<Guid>
    {
        public bool universityVisit { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
