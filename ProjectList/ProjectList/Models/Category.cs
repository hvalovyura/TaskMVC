using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectList.Models
{
    public class Category
    {

        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "error message")]
        [Required]
        public string Name { get; set; }

        //public List<Project> Projects { get; set; }
        //public Category()
        //{
        //    Projects = new List<Project>();
        //}
    }
}
