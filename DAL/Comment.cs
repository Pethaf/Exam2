using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Comment
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTime Created { get; set; }

        public string CommentedBy { get; set; }
    }
}
