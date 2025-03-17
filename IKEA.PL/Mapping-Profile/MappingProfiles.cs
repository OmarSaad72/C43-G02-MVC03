using AutoMapper;
using IKEA.BLL.Models.Department;
using IKEA.PL.View_Models.Department;

namespace IKEA.PL.Mapping_Profile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Employee

            #endregion

            #region Department
            CreateMap<DepartmentVM , CreatedDepartmentDto>();
            CreateMap<DepartmentsDetailsReturnDto , DepartmentVM>();
            CreateMap<DepartmentVM , UpdateDepartmentDto>();
            #endregion
        }
    }
}
