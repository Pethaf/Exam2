
namespace Service.Models
{
    public class ArticleSummaryDTO
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Intro { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public DateTime? Created { get; set; }

        public string ImageName { get; set; }

        public bool Pinned { get; set; }
    }
}
