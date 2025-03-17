using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Generics
{
    public interface IGenericRepo<T> where T : ModelBase 
    {
        IEnumerable<T> GetAll(bool WhithNoTracking = true);
        IQueryable<T> GetAllQuerable();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
