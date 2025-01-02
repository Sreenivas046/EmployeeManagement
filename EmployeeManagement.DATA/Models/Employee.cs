using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.DATA.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public int PhoneNumber { get; set; }
        public string? EmailId { get; set; }
        public ICollection<Name>? Name { get; set; }
        public ICollection<Address>? Address { get; set; }
        public ICollection<AddressProof>? AddressProof { get; set; }
        
    }

    public enum AddressType
    {
        Home = 1,
        Office = 2,
        Other = 3
    }

    public enum AddressProofType
    {
        DL = 1,
        Passport = 2,
        Voter = 3
    }

    public class Name
    {
        [Key]
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string? First { get; set; }
        public string? Middle { get; set; }
        public string? Last { get; set; }
        [ForeignKey("EmpId")]
        public Employee? Employee { get; set; }
    }

    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        public int EmpId { get; set; }
        public AddressType Type { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int PinCode { get; set; }
        [ForeignKey("EmpId")]
        public Employee? Employee { get; set; }
    }

    public class AddressProof
    {
        [Key]
        public int ID { get; set; }
        public int EmpId { get; set; }
        public AddressProofType Type { get; set; }
        public string? DocumentNumber { get; set; }
        [ForeignKey("EmpId")]
        public Employee? Employee { get; set; }
    }

}
