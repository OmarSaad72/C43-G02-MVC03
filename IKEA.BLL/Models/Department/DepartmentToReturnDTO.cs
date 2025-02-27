using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Department
{
    public class DepartmentToReturnDTO  //getAll {Select}
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; //null-forgiving operator
        public string Code { get; set; } = null!; //null-forgiving operator
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}

    }
}
