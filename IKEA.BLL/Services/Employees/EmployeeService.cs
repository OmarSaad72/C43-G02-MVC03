using IKEA.BLL.Models.Common.Enums;
using IKEA.BLL.ModelsDTOS.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeesRepo _Employeerepo;

        public EmployeeService(IEmployeesRepo Employeerepo)
        {
            _Employeerepo = Employeerepo;
        }
        public int CreateEmployee(CreatedEmployeeDto Employee)
        {
            var emp = new Employee()
            {
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                HiringDate = Employee.HiringDate,
                Gender = Employee.Gender,
                EmployeeType = Employee.EmployeeType,
                LastModifiedby = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            return _Employeerepo.Add(emp);
        }

        public bool DeleteEmployee(int id)
        {
            var emp = _Employeerepo.GetById(id);
            if (emp != null)
            {
                int RowsAffected = _Employeerepo.Delete(emp);
                return RowsAffected > 0;
            }
            return false;
        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {
            return _Employeerepo.GetAllQuerable().Where(e => !e.IsDeleted).Select(Employees => new EmployeeToReturnDto
            {
                Id = Employees.Id,
                Name = Employees.Name,
                Age = Employees.Age,
                IsActive = Employees.IsActive,
                Salary = Employees.Salary,
                Email = Employees.Email,
                Gender = Employees.Gender.ToString(),
                EmployeeType = Employees.EmployeeType.ToString()
            }).AsNoTracking().ToList();
        }
        public EmployeesDetailsReturnDto? GetEmployeeById(int Id)
        {
            var Employees = _Employeerepo.GetById(Id);
            if (Employees != null)
            {
                return new EmployeesDetailsReturnDto()
                {
                    Id = Employees.Id,
                    Name = Employees.Name,
                    Age = Employees.Age,
                    IsActive = Employees.IsActive,
                    Salary = Employees.Salary,
                    Email = Employees.Email,
                    Address = Employees.Address,
                    HiringDate = Employees.HiringDate,
                    PhoneNumber = Employees.PhoneNumber,
                    Gender = Employees.Gender.ToString(),
                    EmployeeType = Employees.EmployeeType.ToString(),
                    CreatedBy = Employees.CreatedBy,
                    CreatedOn = Employees.CreatedOn,
                    LastModifiedby = Employees.LastModifiedby,
                    LastModifiedOn = Employees.LastModifiedOn,
                };
            }
            return null;
        }

        public int UpdateEmployee(EditCreateEmployeeDto Employee)
        {
            var employee = new Employee()
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                HiringDate = Employee.HiringDate,
                Gender = Employee.Gender,
                EmployeeType = Employee.EmployeeType,
                LastModifiedby = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };
            return _Employeerepo.Update(employee);
        }
    }
}
