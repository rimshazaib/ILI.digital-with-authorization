using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.Register
{
    public class RegisterDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Email { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
