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
        Task<IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync();
        Task<DepartmentsDetailsReturnDto?> GetDepartmentsByIdAsync(int Id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto department);
        Task<int> UpdateDepartmentAsync(UpdateDepartmentDto departments);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
