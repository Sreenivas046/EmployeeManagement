using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.DTO
{
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

    public class Employee
    {
        public int Id { get; set; }
        public Name? Name { get; set; }
        [Required]
        public string? EmailId { get; set; }
        public Address? Address { get; set; }
        public AddressProof? AddressProof { get; set; }
        public int PhoneNumber { get; set; }
    }

    public class Name
    {
        [Required]
        public string? First { get; set; }
        public string? Middle { get; set; }
        [Required]
        public string? Last { get; set; }
    }

    public class Address
    {
        public int Type { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int PinCode { get; set; }
    }

    public class AddressProof
    {
        public int Type { get; set; }
        public string? DocumentNumber { get; set; }
    }
}
