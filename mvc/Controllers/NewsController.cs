using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }


        public IActionResult StartPage()
        {
            ViewBag.rnd = new Random().Next(1, 4);
            var StartPageContent = new StartPageViewModel();
            StartPageContent.LatestArticles = Service.Services.ArticleService.Instance.GetLatestArticles(5);
            StartPageContent.PinnedArticles = Service.Services.ArticleService.Instance.GetPinnedArticles();
            return View(StartPageContent);
        }


        [HttpPost]
        public IActionResult NewsPage(ArticleViewModel article)
        {
            var rightArticle = Service.Services.ArticleService.Instance.GetById(article.theComment.ArticleId);
            article.theArticle = rightArticle;
                if (ModelState.IsValid)
                {
                
                Service.Services.ArticleService.Instance.AddComment(
                        new Service.Models.CreateCommentDTO
                        {
                            Value = article.theComment.Value,
                            CommentedBy = article.theComment.CommentedBy,
                            ArticleId = article.theComment.ArticleId
                        });
                return new RedirectResult($"~/News/{rightArticle.Id}-{rightArticle.Title}");
            }
            return View(article);
                   
        }

        public IActionResult NewsPage(string blob)
        {
            try
            {
                ViewBag.rnd = new Random().Next(1, 4);
                ViewBag.articleID = blob.Substring(0, 36);
                var articleTItle = blob.Substring(38);
                var ArticleDetailView = new ArticleViewModel();
                ArticleDetailView.theArticle = Service.Services.ArticleService.Instance.GetById(new Guid(ViewBag.articleID));
                return View(ArticleDetailView);

            }
            catch (Exception err)
            {
                if (blob is null)
                {
                    return new RedirectResult("");
                }
                return BadRequest("Invalid Article Request");
            }
        }
        [HttpPost("form")]
        public IActionResult form(ArticleViewModel article)
        {
            var theArticle = Service.Services.ArticleService.Instance.GetById(article.theComment.ArticleId);
            if (ModelState.IsValid)
            {
                Service.Services.ArticleService.Instance.AddComment(
                    new Service.Models.CreateCommentDTO
                    {
                        Value = article.theComment.Value,
                        CommentedBy = article.theComment.CommentedBy,
                        ArticleId = article.theComment.ArticleId
                    });
            }
                return RedirectToAction("NewsPage", new { blob = $"{theArticle.Id}{theArticle.Title}" });
            
        }
    } 
}