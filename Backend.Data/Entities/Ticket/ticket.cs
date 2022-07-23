using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Entities.Ticket
{
    public class Ticket
    {
        [Key]
 
        public int Tid { get; set; }
        public int Price { get; set; }
        public int MovieId { get; set; }
        public Movie Movies { get; set; }
    }
}
