using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CapstoneWIE.DataLayer.Models.Enums;

namespace CapstoneWIE.DataLayer.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        [Required]
        [StringLength(128)]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public BlogState BlogState { get; set; }

        public DateTime PostDate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
