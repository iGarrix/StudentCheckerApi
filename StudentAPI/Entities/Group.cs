namespace StudentAPI.Entities
{
    public class Group : BaseModel<Guid>
    {
        public string Name { get; set; }
    }
}
