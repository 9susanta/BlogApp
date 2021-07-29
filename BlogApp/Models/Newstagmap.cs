using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Newstagmap
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? TagId { get; set; }
    }
}
