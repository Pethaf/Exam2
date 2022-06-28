
namespace DAL
{
    public class Article
    {
        public Guid Id { get; set; } 

        public string Title { get; set; }
        public string Intro { get; set; }

        public IEnumerable<Block> Blocks { get; set;}

        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        public IEnumerable<Comment> Comments { get; set;}    

        public DateTime Created { get; set; }

        public string ImageName { get; set; }

        public bool Pinned { get; set; }
    }
}
