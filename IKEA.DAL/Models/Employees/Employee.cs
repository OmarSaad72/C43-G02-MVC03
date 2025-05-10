using IKEA.BLL.Models.Common.Enums;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Employees
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? Image { get; set; }


        #region Work 
        /******************************Department Relation (Work 1: M{1})******************************/
        public virtual Department? Department { get; set; } //Navigational Property ==> One Side
        public int? DepartmentId { get; set; }
        #endregion
    }
}
