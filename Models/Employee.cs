using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFWorkforce.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public bool IsSupervisor { get; set; }
        [Display(Name = "Computer Id")]
        public int ComputerId { get; set; }
   
        public Computer Computer { get; set; }
        public Department Department { get; set; }
      
    }
}
