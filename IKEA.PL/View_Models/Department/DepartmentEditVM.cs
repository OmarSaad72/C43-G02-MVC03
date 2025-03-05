using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.View_Models.Department
{
    public class DepartmentEditVM
    {
        public string Name { get; set; } = null!; //null-forgivenees operator
        public string Code { get; set; } = null!; //null-forgivenees operator
        public string? Description { get; set; }
        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
