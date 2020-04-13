using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectList.Models
{
    public class ProjectsContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public ProjectsContext(DbContextOptions<ProjectsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
