using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapstoneWIE.DataLayer.Models
{
    public class Tag    
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}