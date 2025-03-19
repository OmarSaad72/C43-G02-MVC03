using IKEA.BLL.Common.Services.AttachmentService;
using IKEA.BLL.Models.Common.Enums;
using IKEA.BLL.ModelsDTOS.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Employees;
using IKEA.DAL.Presistance.UnitOfWork;
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
        private readonly IUnitOfWork _unit;
        private readonly IAttachmentService _attachmentService;

        //private readonly IEmployeesRepo _Employeerepo;

        //public EmployeeService(IEmployeesRepo Employeerepo)
        //{
        //    _Employeerepo = Employeerepo;
        //}
        public EmployeeService(IUnitOfWork unit, IAttachmentService attachmentService)
        {
            _unit = unit;
            _attachmentService = attachmentService;
        }
        public async Task<int> CreateEmployeeAsync(EditCreateEmployeeDto Employee)
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
                DepartmentId = Employee.DepartmentId,
            };
            if (Employee.Image != null)
            {
                emp.Image = await _attachmentService.UploadAsync(Employee.Image, "Images");
            }
            _unit.EmployeesRepo.Add(emp);
            return await _unit.CompleteAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var empUOW = _unit.EmployeesRepo;
            var emp = await empUOW.GetByIdAsync(id);
            if (emp != null)
            {
                empUOW.Delete(emp);
            }
            return await _unit.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue)
        {
            return await _unit.EmployeesRepo.GetAllQuerable().Where(e => !e.IsDeleted &&
            (string.IsNullOrEmpty(SearchValue)
            || e.Name.ToLower().Contains(SearchValue.ToLower())))
                .Select(Employees => new EmployeeToReturnDto
                {
                    Id = Employees.Id,
                    Name = Employees.Name,
                    Age = Employees.Age,
                    IsActive = Employees.IsActive,
                    Salary = Employees.Salary,
                    Email = Employees.Email,
                    Gender = Employees.Gender.ToString(),
                    EmployeeType = Employees.EmployeeType.ToString(),
                    Department = Employees.Department.Name,  // Lazy Loading
                    Image = Employees.Image
                }).ToListAsync();
        }
        public async Task<EmployeesDetailsReturnDto?> GetEmployeeByIdAsync(int Id)
        {
            var Employees = await _unit.EmployeesRepo.GetByIdAsync(Id);
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
                    Department = Employees?.Department?.Name,  // Lazy Loading
                    Image = Employees.Image
                };
            }
            return null;
        }

        public async Task<int> UpdateEmployeeAsync(EditCreateEmployeeDto Employee)
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
                LastModifiedOn = DateTime.UtcNow,
                DepartmentId = Employee.DepartmentId
            };
            if (Employee.Image != null)
            {
                employee.Image = await _attachmentService.UploadAsync(Employee.Image, "Images");
            }
            _unit.EmployeesRepo.Update(employee);
            return await _unit.CompleteAsync();
        }
    }
}
