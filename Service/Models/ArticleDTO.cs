using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class ArticleDTO
    {
        public Guid? Id { get; set; }

        public string Title { get; set; }

        public string Intro { get; set; }

        public IEnumerable<BlockDTO>? Blocks { get; set; }

        public AuthorDTO? Author { get; set; }

        public IEnumerable<CommentDTO>? Comments { get; set; }

        public DateTime? Created { get; set; }

        public string ImageName { get; set; }

        public bool Pinned { get; set; }

        public CreateCommentDTO NewComment { get; set; }
    }
}
