using Service.Models;

namespace mvc.Models
{
    public class StartPageViewModel
    {
        public IEnumerable<ArticleSummaryDTO> LatestArticles { get; set; }
        public IEnumerable<ArticleSummaryDTO> PinnedArticles { get; set; }
        public StartPageViewModel()
        {
            LatestArticles = new List<ArticleSummaryDTO>();
            PinnedArticles = new List<ArticleSummaryDTO>();
        }
    }
}
