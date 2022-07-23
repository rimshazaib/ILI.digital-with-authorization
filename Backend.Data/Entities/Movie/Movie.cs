
using Backend.Data.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

       // [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

       // [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public ICollection<Ticket> Ticket{ get; set; }
        
    }
}