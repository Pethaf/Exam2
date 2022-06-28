using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class CreateAuthorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ImageName { get; set; }

        public string? Mail { get; set; }

        public string? TwitterUserName { get; set; }
    }
}
