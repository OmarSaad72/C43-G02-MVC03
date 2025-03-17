using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Department
{
    public interface IDepartmentRepo : IGenericRepo<Models.Departments.Department>
    {
       
    }
}
