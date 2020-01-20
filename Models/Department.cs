using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFWorkforce.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Budget { get; set; }

        public List<Employee> Employees { get; set; }
       
    }
}