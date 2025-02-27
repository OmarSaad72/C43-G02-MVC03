using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Department
{
    public interface IDepartmentRepo
    {
        IEnumerable<Departments> GetAll(bool WhithNoTracking = true);
        IQueryable<Departments> GetAllQuerable();
        Departments? GetById(int id);
        int Add(Departments entity);
        int Update(Departments entity);
        int Delete(Departments entity);
    }
}
