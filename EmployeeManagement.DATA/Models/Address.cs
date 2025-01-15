using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.DATA.Enums;

namespace EmployeeManagement.DATA.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        [ForeignKey("Employee")]
        public Guid EmpId { get; set; }
        public AddressType Type { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int PinCode { get; set; }
        public Employee? Employee { get; set; }
    }
}
