namespace KIPServiceTestTask.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public User(Guid Id, string? Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
