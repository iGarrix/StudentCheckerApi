namespace StudentAPI.Entities
{
    public class Schedule : BaseModel<Guid>
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
