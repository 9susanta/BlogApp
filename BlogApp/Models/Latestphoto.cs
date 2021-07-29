using System;
using System.Collections.Generic;

#nullable disable

namespace BlogApp.Models
{
    public partial class Latestphoto
    {
        public long Id { get; set; }
        public string EnglishTitle { get; set; }
        public string OdiaTitle { get; set; }
        public string Thumbnail210 { get; set; }
        public string Thumbnail279 { get; set; }
        public string Thumbnail86 { get; set; }
        public string HeaderImageName { get; set; }
        public string EngShortDesc { get; set; }
        public string OdshortDesc { get; set; }
        public string Odcontent { get; set; }
        public string SeoMeta { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? PostedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Tags { get; set; }
        public int? PostedMonth { get; set; }
        public int? PostedYear { get; set; }
        public int? Frequency { get; set; }
        public string SlugUrl { get; set; }
        public bool? IsReviewed { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? ReviewedBy { get; set; }
    }
}
