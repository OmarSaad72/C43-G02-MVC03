using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Generics
{
    public class GenericRepo<T> : IGenericRepo<T> where T : ModelBase
    {
        private readonly AppDbContext _DbContext;
        public GenericRepo(AppDbContext dbContext) //Ask CLR for object from AppDbContext implicitly
        {
            _DbContext = dbContext;
        }
        public void Add(T entity)
        {
            _DbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            //_DbContext.Set<T>().Remove(entity);
            //return _DbContext.SaveChanges();
            entity.IsDeleted = true;
            _DbContext.Set<T>().Update(entity);
        }

        public IEnumerable<T> GetAll(bool WhithNoTracking = true)
        {
            if (WhithNoTracking)
            {
                return _DbContext.Set<T>().Where(Z => !Z.IsDeleted).AsNoTracking().ToList();
            }
            return _DbContext.Set<T>().Where(Z => !Z.IsDeleted).ToList();
        }

        public IQueryable<T> GetAllQuerable()
        {
            return _DbContext.Set<T>();
        }

        public T? GetById(int id)
        {
            return _DbContext.Set<T>().Find(id);
            //var dep = _DbContext.Department.FirstOrDefault(d => d.Id == id);
            //return dep;
        }

        public void Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
        }
    }
}
