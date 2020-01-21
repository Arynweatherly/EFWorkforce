using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFWorkforce.Models.ViewModels
{
    public class DepartmentIndexViewModel
    {
     
        public string Name { get; set; }
        public int Id { get; set; }
        public int Budget { get; set; }
        public int EmployeeCount { get; set; }
        public List<Department> Departments { get; set; }
        //public List<int> EmployeeCount { get; set; }


    }
}
