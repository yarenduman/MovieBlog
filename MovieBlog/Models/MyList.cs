using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieBlog.Models
{
    public class MyList
    {
        public MyList()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter the title of movie")]
        [MaxLength(200)]
        [Display(Name = "Title")]
        public string Movie { get; set; }

        [Required(ErrorMessage = "Please, select the genre of the movie")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        [Required(ErrorMessage = "Please, enter the director of the movie")]
        [MaxLength(200)]
        public string Director { get; set; }

        [Required(ErrorMessage = "Please, enter the released year of the movie")]
        [MaxLength(200)]
        [Display(Name = "Release Year")]
        public string Year { get; set; }

        [Display(Name = "Is Watched")]
        public bool IsCompleted { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CompletedDate { get; set; }

        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}

