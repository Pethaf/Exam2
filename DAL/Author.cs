
namespace DAL
{
    public class Author
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageName { get; set; }

        public string? Mail { get; set; }

        public string? TwitterUserName { get; set; }
    }
}
