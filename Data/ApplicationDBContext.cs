using EFWorkforce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFWorkforce.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Computer> Computer { get; set; }
    }
}
