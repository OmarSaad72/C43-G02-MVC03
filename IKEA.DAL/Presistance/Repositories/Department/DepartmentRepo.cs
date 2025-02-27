using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Department
{
    internal class DepartmentRepo : IDepartmentRepo
    {
        private readonly AppDbContext _DbContext;
        public DepartmentRepo(AppDbContext dbContext) //Ask CLR for object from AppDbContext implicitly
        {
            _DbContext = dbContext;
        }
        public int Add(Departments entity)
        {
            _DbContext.Department.Add(entity); 
            return _DbContext.SaveChanges();
        }

        public int Delete(Departments entity)
        {
            _DbContext.Department.Remove(entity);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Departments> GetAll(bool WhithNoTracking = true)
        {
            if (WhithNoTracking)
            {
                return _DbContext.Department.AsNoTracking().ToList();
            }
            return _DbContext.Department.ToList();
        }

        public IQueryable<Departments> GetAllQuerable()
        {
            return _DbContext.Department;
        }

        public Departments? GetById(int id)
        {
            return _DbContext.Department.Find(id);
            //var dep = _DbContext.Department.FirstOrDefault(d => d.Id == id);
            //return dep;
        }

        public int Update(Departments entity)
        {
            _DbContext.Department.Update(entity);
            return _DbContext.SaveChanges();
        }
    }
}
