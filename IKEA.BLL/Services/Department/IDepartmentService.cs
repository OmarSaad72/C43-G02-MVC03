using IKEA.BLL.Models.Department;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Department
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDTO> GetAllDepartments();
        DepartmentsDetailsReturnDto? GetDepartmentsById(int Id);
        int CreateDepartment(CreatedDepartmentDto department);
        int UpdateDepartment(UpdateDepartmentDto departments);
        bool DeleteDepartment(int id);
    }
}
