using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Block
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public int Order { get; set; }
    }
}
