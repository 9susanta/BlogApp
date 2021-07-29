using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
    }
}
