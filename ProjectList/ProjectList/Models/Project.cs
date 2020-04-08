using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectList.Models
{
    public class Project
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(100, ErrorMessage = "error message")]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [StringLength(1000, ErrorMessage = "error message")]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
