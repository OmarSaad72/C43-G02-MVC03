using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Department
{
    public class DepartmentRepo : GenericRepo<Models.Departments.Department>, IDepartmentRepo
    {
        public DepartmentRepo(AppDbContext dbContext): base(dbContext) 
        {
            
        }
    }
}
