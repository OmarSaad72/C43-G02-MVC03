using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Department;
using IKEA.DAL.Presistance.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Employees
{
    public class EmployeesRepo : GenericRepo<Employee>, IEmployeesRepo
    {
        public EmployeesRepo(AppDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
