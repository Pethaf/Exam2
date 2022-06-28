using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class CreateArticleDTO
    {
        public string Title { get; set; }

        public string Intro { get; set; }

        public List<UpdateOrCreateBlockDTO> Blocks { get; set; } = new List<UpdateOrCreateBlockDTO>();

        public Guid AuthorId { get; set; }

        public string ImageName { get; set; }

        public bool Pinned { get; set; }
    }
}
