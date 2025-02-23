using System;
using System.Collections.Generic;
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
    }
}
