using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class UpdateArticleDTO
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Intro { get; set; }

        public IEnumerable<UpdateOrCreateBlockDTO>? Blocks { get; set; }

        public Guid? AuthorId { get; set; }

        public IEnumerable<CommentDTO>? Comments { get; set; }

        public string? ImageName { get; set; }

        public bool? Pinned { get; set; }
    }
}
