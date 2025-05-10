using AutoMapper;
using IKEA.BLL.Common.Services.AttachmentService;
using IKEA.BLL.Models.Department;
using IKEA.BLL.ModelsDTOS.Employees;
using IKEA.PL.View_Models.Department;

namespace IKEA.PL.Mapping_Profile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Employee
            CreateMap<EmployeesDetailsReturnDto, EditCreateEmployeeDto>();
            CreateMap<EditCreateEmployeeDto, EditCreateEmployeeDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
            CreateMap<EditCreateEmployeeDto, EditCreateEmployeeDto>();
            #endregion

            #region Department
            CreateMap<DepartmentVM, CreatedDepartmentDto>();
            CreateMap<DepartmentsDetailsReturnDto, DepartmentVM>();
            CreateMap<DepartmentVM, UpdateDepartmentDto>();
            #endregion
        }
    }
}
