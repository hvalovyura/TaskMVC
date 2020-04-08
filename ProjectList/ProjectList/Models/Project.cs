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
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "error message")]
        public string Name { get; set; }
        public int Price { get; set; }
        [StringLength(1000, ErrorMessage = "error message")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
