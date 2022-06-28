using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class CreateCommentDTO
    {

        [Required(ErrorMessage ="Kan inte vara tom")]
        public string Value { get; set; }

        [Required(ErrorMessage ="Kan inte vara tom")]
        [RegularExpression(@"[A-Za-z]+@(\.|[A-Za-z])+", ErrorMessage ="Ogiltig e-postadress")]
        public string CommentedBy { get; set; }

        public Guid ArticleId { get; set; }
    }
}
