using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories.Department;
using IKEA.DAL.Presistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //EmployeesRepo = new EmployeesRepo(_appDbContext);
            //DepartmentRepo = new DepartmentRepo(_appDbContext);
        }
        public IEmployeesRepo EmployeesRepo => new EmployeesRepo(_appDbContext); // create instance
        public IDepartmentRepo DepartmentRepo => new DepartmentRepo(_appDbContext); // create instance

        public async Task<int> CompleteAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await _appDbContext.DisposeAsync();
        }
    }
}
