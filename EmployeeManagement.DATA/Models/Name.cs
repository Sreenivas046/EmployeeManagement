using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DATA.Models
{
    public class Name
    {
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public Guid EmpId { get; set; }
        public string? First { get; set; }
        public string? Middle { get; set; }
        public string? Last { get; set; }
        public Employee? Employee { get; set; }
    }
}
