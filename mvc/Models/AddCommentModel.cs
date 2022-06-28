namespace mvc.Models
{
    public class AddCommentModel
    {
            public string Value { get; set; }

            public string Email { get; set; }

            public Guid ArticleId { get; set; }
    }
}
