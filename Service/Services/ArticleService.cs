using DAL;
using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.Services
{
    public class ArticleService 
    {
        private static ArticleService? instance = null;

        public static ArticleService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ArticleService();
                }
                return instance;
            }
        }

        public ArticleDTO GetById(Guid articleId) {
            using (var context = new ExamContext()) {
                var article = context.Articles
                    .Include(x => x.Comments)
                    .Include(x => x.Blocks)
                    .Include(x => x.Author)
                    .FirstOrDefault(x => x.Id == articleId);

                if (article == null)
                    throw new NullReferenceException($"No article found, id:{articleId}");

                return new ArticleDTO
                    {
                        Id = article.Id,
                        Title = article.Title,
                        Intro = article.Intro,
                        Created = article.Created,
                        Pinned = article.Pinned,
                        ImageName = article.ImageName,
                        Author = new AuthorDTO
                        {
                            Id = article.Author.Id,
                            FirstName = article.Author.FirstName,
                            LastName = article.Author.LastName,
                            ImageName = article.Author.ImageName,
                            Mail = article.Author.Mail,
                            TwitterUserName = article.Author.TwitterUserName,
                        },
                        Comments = article.Comments.Select(c => new CommentDTO
                        {
                            Id = c.Id,
                            Value = c.Value,
                            Created = c.Created,
                            CommentedBy = c.CommentedBy
                        }).OrderByDescending(comment => comment.Created),
                        Blocks = article.Blocks.Select(b => new BlockDTO
                        {
                            Id = b.Id,
                            Type = b.Type,
                            Value = b.Value,
                            Order = b.Order
                        }).OrderBy(block => block.Order),
                    };
            }
        }

        public IEnumerable<ArticleSummaryDTO> GetLatestArticles(int numberOfArticles)
        {
            using (var context = new ExamContext())
            {
                return context.Articles
                    .Include(x => x.Comments)
                    .Include(x => x.Author)
                    .Select(ArticleToSummaryDTO)
                    .OrderByDescending(x => x.Created)
                    .Take(numberOfArticles)
                    .ToList();
            }
        }

        public IEnumerable<ArticleSummaryDTO> GetPinnedArticles()
        {
            using (var context = new ExamContext())
            {
                return context.Articles
                    .Include(x => x.Author)
                    .OrderByDescending(x => x.Pinned).ThenByDescending(x => x.Created)
                    .Take(6)
                    .Select(ArticleToSummaryDTO)
                    .ToList();
            }
        }

        public IEnumerable<ApiArticleDTO> GetAllArticles()
        {
            using (var context = new ExamContext())
            {
                return context.Articles
                    .Include(x => x.Blocks)
                    .Select(x => new ApiArticleDTO
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Intro = x.Intro,
                        AuthorId = x.AuthorId,
                        ImageName = x.ImageName,
                        Pinned = x.Pinned,
                        Blocks = x.Blocks
                            .Select(block => new BlockDTO()
                            {
                                Id = block.Id,
                                Type = block.Type,
                                Value = block.Value,
                                Order = block.Order
                            })
                            .OrderBy(block => block.Order)
                            .ToList()
                    })
                    .OrderBy(article => article.Title)
                    .ToList();
            }
        }

        public void CreateArticle(CreateArticleDTO articleDto)
        {
            using (var context = new ExamContext())
            {
                var article = new Article
                {
                    Id = Guid.NewGuid(),
                    Title = articleDto.Title,
                    Intro = articleDto.Intro,
                    AuthorId = articleDto.AuthorId,
                    ImageName = articleDto.ImageName,
                    Created = DateTime.Now,
                    Pinned = articleDto.Pinned
                };

                var blockList = new List<Block>();
                var imageFileNames = ImageService.Instance.GetAll();
                foreach (var block in articleDto.Blocks)
                {
                    if (block.Type == "image" && !imageFileNames.Contains(block.Value))
                    {
                        throw new ArgumentException("Provided imageName does not exist on server. Image name: " + block.Value);
                    }
                    blockList.Add(
                        new Block()
                        {
                            Id = Guid.NewGuid(),
                            Type = block.Type,
                            Value = block.Value,
                            Order = block.Order
                        }
                    );
                }
                article.Blocks = blockList;

                context.Add(article);
                context.SaveChanges();
            }
        }

        public void DeleteArticle(Guid id)
        {
            using (var context = new ExamContext())
            {
                var article = context.Articles.FirstOrDefault(x => x.Id == id);
                if (article == null)
                    throw new NullReferenceException($"No Article found with Id: {id}");

                context.Remove(article);
                context.SaveChanges();
            }
        }

        public void UpdateArticle(UpdateArticleDTO articleDto)
        {
            using (var context = new ExamContext())
            {
                var article = context.Articles.Include(x => x.Blocks).FirstOrDefault(x => x.Id == articleDto.Id);

                if (article == null)
                    throw new NullReferenceException($"No Article found with Id: {articleDto.Id}");

                if(!string.IsNullOrEmpty(articleDto.Title))
                    article.Title = articleDto.Title;

                if (!string.IsNullOrEmpty(articleDto.Intro))
                    article.Intro = articleDto.Intro;

                if (articleDto.AuthorId != null)
                    article.AuthorId = articleDto.AuthorId.Value;

                if (articleDto.Pinned != null)
                    article.Pinned = articleDto.Pinned.Value;

                if (articleDto.ImageName != null)
                    article.ImageName = articleDto.ImageName;

                if (articleDto.Blocks != null)
                {
                    var currentBlocks = article.Blocks.ToList();

                    var imageFileNames = ImageService.Instance.GetAll();
                    var newBlockList = new List<Block>();
                    foreach (var block in articleDto.Blocks)
                    {
                        if (block.Type == "image" && !imageFileNames.Contains(block.Value))
                        {
                            throw new ArgumentException("Provided imageName does not exist on server. Image name: " + block.Value);
                        }

                        if (block.Id.HasValue)
                        {
                            var existingBlock = article.Blocks
                            .First(p => p.Id == block.Id);
                            newBlockList.Add(existingBlock);
                            existingBlock.Type = block.Type;
                            existingBlock.Value = block.Value;
                            existingBlock.Order = block.Order;
                        }
                        else
                        {
                            var newBlock = new Block
                            {
                                Id = Guid.NewGuid(),
                                Value = block.Value,
                                Type = block.Type,
                                Order = block.Order
                            };
                            newBlockList.Add(newBlock);
                            context.Add(newBlock);
                        }
                    }
                    article.Blocks = newBlockList;

                    var removedBlocks = currentBlocks.Where(block => !article.Blocks.Contains(block)).ToList();
                    context.RemoveRange(removedBlocks);
                }

                context.Articles.Update(article);
                context.SaveChanges();
                
            }
        }

        public void AddComment(CreateCommentDTO comment) {
            
            using (var context = new ExamContext())
            {
                var article = context.Articles.Include(x => x.Comments).FirstOrDefault(x => x.Id == comment.ArticleId);
                if (article == null)
                    throw new NullReferenceException($"No Article found with Id: {comment.ArticleId}");

                var oldComments = article.Comments.ToList();
                oldComments.Add(new Comment
                {
                    Value = comment.Value,
                    CommentedBy = comment.CommentedBy,
                    Created = DateTime.Now
                });
                article.Comments = oldComments;

                context.Update(article);
                context.SaveChanges();
            }
        }

        private ArticleSummaryDTO ArticleToSummaryDTO(Article article)
        {
            return new ArticleSummaryDTO
            {
                Id = article.Id,
                Title = article.Title,
                Intro = article.Intro,
                Created = article.Created,
                AuthorFirstName = article.Author.FirstName,
                AuthorLastName = article.Author.LastName,
                ImageName = article.ImageName,
                Pinned = article.Pinned
            };
        }
    }
}
