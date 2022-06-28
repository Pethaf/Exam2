using Service.Models;

namespace mvc.Models
{
    public class ArticleViewModel
    {
        public ArticleDTO? theArticle { get; set; }
        public CreateCommentDTO theComment { get; set; }
    }
}
