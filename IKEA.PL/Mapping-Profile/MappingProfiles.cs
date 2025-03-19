using AutoMapper;
using IKEA.BLL.Models.Department;
using IKEA.BLL.ModelsDTOS.Employees;
using IKEA.PL.View_Models.Department;
using IKEA.PL.View_Models.Employee;

namespace IKEA.PL.Mapping_Profile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Employee
            CreateMap<EditCreateEmployeeDto, EditCreateEmployeeDto>();
            CreateMap<EmployeesDetailsReturnDto, EditCreateEmployeeDto>();
            CreateMap<EditCreateEmployeeDto, EmployeeEditVM>();
            #endregion

            #region Department
            CreateMap<DepartmentVM , CreatedDepartmentDto>();
            CreateMap<DepartmentsDetailsReturnDto , DepartmentVM>();
            CreateMap<DepartmentVM , UpdateDepartmentDto>();
            #endregion
        }
    }
}
