using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Entities
{
    public class BaseModel<Type>
    {
        [Key]
        public Type Id { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
    }
}
