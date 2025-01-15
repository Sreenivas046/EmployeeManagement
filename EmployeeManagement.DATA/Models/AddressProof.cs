using EmployeeManagement.DATA.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.DATA.Models
{
    public class AddressProof
    {
        public int ID { get; set; }
        [ForeignKey("Employee")]
        public Guid EmpId { get; set; }
        public AddressProofType Type { get; set; }
        public string? DocumentNumber { get; set; }
        public Employee? Employee { get; set; }
    }
}
