﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class BlockDTO
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
        public int Order { get; set; }
    }
}
