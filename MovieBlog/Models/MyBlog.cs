using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBlog.Models
{
    public class MyBlog
    {
        public MyBlog()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter the heading of the content")]
        [MaxLength(200)]
        public string Heading { get; set; }

        [Required(ErrorMessage = "Please, enter the content")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1500)]
        public string Content { get; set; }

        [Display(Name = "Is Published")]
        public bool IsPublished { get; set; }

        public DateTime CreatedDate { get; set; }

        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }

    }
}
