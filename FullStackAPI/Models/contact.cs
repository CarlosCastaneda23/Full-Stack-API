namespace FullStackAPI.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public long PhoneNumber { get; set; }
        public String TextComment { get; set; }

    }
}
