using System;
using System.ComponentModel.DataAnnotations;

namespace CapstoneWIE.DataLayer.Models
{
    public class Page
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        [Required]
        [StringLength(128)]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
