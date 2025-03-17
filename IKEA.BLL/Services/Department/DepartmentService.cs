using IKEA.BLL.Models.Department;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Department;
using IKEA.DAL.Presistance.UnitOfWork;
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
        private readonly IUnitOfWork _unit;

        //private readonly IDepartmentRepo _departmentrepo;

        //public DepartmentService(IDepartmentRepo departmentrepo)
        //{
        //    _departmentrepo = departmentrepo;
        //}
        public DepartmentService(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public int CreateDepartment(CreatedDepartmentDto department)
        {
            var dep = new DAL.Models.Departments.Department()
            {
                Code = department.Code,
                Description = department.Description,
                Name = department.Name,
                CreationDate = department.CreationDate,
                LastModifiedby = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            _unit.DepartmentRepo.Add(dep);
            return _unit.Complete();
        }

        public bool DeleteDepartment(int id)
        {
            var department = _unit.DepartmentRepo;
            var dep = department.GetById(id);
            if (dep != null)
            {
                department.Delete(dep);
            }
            return _unit.Complete() > 0;
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
            var department = _unit.DepartmentRepo.GetAllQuerable().Where(d => !d.IsDeleted).Select(d => new DepartmentToReturnDTO
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
            var department = _unit.DepartmentRepo.GetById(Id);
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
            var dep = new DAL.Models.Departments.Department()
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
            _unit.DepartmentRepo.Update(dep);
            return _unit.Complete();
        }
    }
}
