using IKEA.BLL.Services.Department;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet] //Default 
        public IActionResult Index() //Master Action
        {
            var dep =_departmentService.GetAllDepartments();
            return View(dep);
        }
    }
}
