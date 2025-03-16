using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Departments
{
    public class Departments :ModelBase
    {
        public string Name { get; set; } = null!; //null-forgiving operator
        public string Code { get; set; } = null!; //null-forgiving operator
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }

        #region Work
        //Works ==> 1 : M {M}
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>(); //   ==> Side Many

        #endregion

    }
}
