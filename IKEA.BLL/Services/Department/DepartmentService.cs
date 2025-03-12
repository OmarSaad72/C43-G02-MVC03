using IKEA.BLL.Models.Department;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data.Migrations;
using IKEA.DAL.Presistance.Repositories.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _departmentrepo;

        public DepartmentService(IDepartmentRepo departmentrepo)
        {
            _departmentrepo = departmentrepo;
        }
        public int CreateDepartment(CreatedDepartmentDto department)
        {
            var dep = new Departments()
            {
                Code = department.Code,
                Description = department.Description,
                Name = department.Name,
                CreationDate = department.CreationDate,
                LastModifiedby = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            return _departmentrepo.Add(dep);
        }

        public bool DeleteDepartment(int id)
        {
            var dep = _departmentrepo.GetById(id);
            if (dep != null)
            {
                int RowsAffected = _departmentrepo.Delete(dep);
                return RowsAffected > 0;
            }
            return false;
        }

        public IEnumerable<DepartmentToReturnDTO> GetAllDepartments()
        {
            //var department = _departmentrepo.GetAll(); // IEnumerable<Department> ==> IEnumerable<DepartmentToReturnDTO>
            ////Mapping: Department ===> DepartmentToReturnDto
            //foreach (var item in department)
            //{
            //    yield return new DepartmentToReturnDTO()
            //    {
            //        Description = item.Description,
            //        CreationDate = item.CreationDate,
            //        Id = item.Id,
            //        Name = item.Name,
            //    };
            //}
            var department = _departmentrepo.GetAllQuerable().Where(d => !d.IsDeleted).Select(d => new DepartmentToReturnDTO
            {
                Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                //Description = d.Description,
                CreationDate = d.CreationDate
            }).AsNoTracking().ToList();
            return department;
        }

        public DepartmentsDetailsReturnDto? GetDepartmentsById(int Id)
        {
            var department = _departmentrepo.GetById(Id);
            if (department != null)
            {
                return new DepartmentsDetailsReturnDto()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedby = department.LastModifiedby,
                    LastModifiedOn = department.LastModifiedOn,
                    Description = department.Description,
                    IsDeleted = department.IsDeleted,
                    CreationDate = department.CreationDate,
                };
            }
            return null;
        }

        public int UpdateDepartment(UpdateDepartmentDto departments)
        {
            var dep = new Departments()
            {
                Id = departments.Id,
                Code = departments.Code,
                Description = departments.Description,
                Name = departments.Name,
                CreationDate = departments.CreationDate,
                LastModifiedby = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            return _departmentrepo.Update(dep);
        }
    }
}
