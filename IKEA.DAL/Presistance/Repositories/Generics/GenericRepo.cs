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

        public async Task<IEnumerable<T>> GetAllAsync(bool WhithNoTracking = true)
        {
            if (WhithNoTracking)
            {
                return await _DbContext.Set<T>().Where(Z => !Z.IsDeleted).AsNoTracking().ToListAsync();
            }
            return await _DbContext.Set<T>().Where(Z => !Z.IsDeleted).ToListAsync();
        }

        public IQueryable<T> GetAllQuerable()
        {
            return _DbContext.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _DbContext.Set<T>().FindAsync(id);
            //var dep = _DbContext.Department.FirstOrDefault(d => d.Id == id);
            //return dep;
        }

        public void Update(T entity)
        {
            _DbContext.Set<T>().Update(entity);
        }
    }
}
