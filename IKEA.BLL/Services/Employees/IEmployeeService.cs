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
        IEnumerable<EmployeeToReturnDto> GetAllEmployees();
        EmployeesDetailsReturnDto? GetEmployeeById(int Id);
        int CreateEmployee(CreatedEmployeeDto Employee);
        int UpdateEmployee(UpdateEmployeeDto Employees);
        bool DeleteEmployee(int id);
    }
}
