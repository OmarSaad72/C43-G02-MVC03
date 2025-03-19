using IKEA.DAL.Presistance.Repositories.Department;
using IKEA.DAL.Presistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public  IEmployeesRepo EmployeesRepo { get;  }
        public IDepartmentRepo DepartmentRepo { get; }
        Task<int> CompleteAsync();
    }
}
