using IKEA.BLL.ModelsDTOS.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue);
         Task<EmployeesDetailsReturnDto?> GetEmployeeByIdAsync(int Id);
         Task<int> CreateEmployeeAsync(EditCreateEmployeeDto Employee);
         Task<int> UpdateEmployeeAsync(EditCreateEmployeeDto Employees);
         Task<bool> DeleteEmployeeAsync(int id);
    }
}
