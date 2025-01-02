using EmployeeManagement.DATA.Models;

namespace EmployeeManagement.DATA.Data
{
    public static class EmployeeDataSeeder
    {
        public static void Seed(EmployeeDbContext context)
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    EmpId = 1,
                    Name = new List<Name>(){new() { Id= 1, EmpId = 1, First = "John", Middle = "M", Last = "Doe" } },
                    EmailId = "john.doe@example.com",
                    Address = new List<Address>(){new()
                    {
                        AddressID = 1,
                        EmpId = 1,
                        Type = AddressType.Home,
                        Line1 = "123 Main St",
                        Line2 = "Apt 4B",
                        Line3 = "",
                        City = "New York",
                        State = "NY",
                        Country = "USA",
                        PinCode = 10001
                    } },
                    AddressProof = new List<AddressProof>(){new ()
                    {
                        ID = 1,
                        EmpId= 1,
                        Type = AddressProofType.DL,
                        DocumentNumber = "DL1234567890"
                    } },
                    PhoneNumber = 1234567890
                },
                new Employee
                {
                    EmpId = 2,
                    Name = new List<Name>(){new() { Id = 2,EmpId = 2, First = "Jane", Middle = "A", Last = "Smith" } },
                    EmailId = "jane.smith@example.com",
                    Address = new List<Address>(){new ()
                    {
                        AddressID = 2,
                        EmpId = 2,
                        Type = AddressType.Office,
                        Line1 = "456 Business Rd",
                        Line2 = "Suite 101",
                        Line3 = "",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PinCode = 90001
                    } },
                    AddressProof = new List<AddressProof>(){ new AddressProof()
                    {
                        ID = 2,
                        EmpId = 2,
                        Type = AddressProofType.Passport,
                        DocumentNumber = "P123456789"
                    } },
                    PhoneNumber = 123456789
                },
                new Employee
                {
                    EmpId = 3,
                    Name = new List<Name>(){new() { Id = 3,EmpId = 3, First = "Sree", Middle = "A", Last = "S" } },
                    EmailId = "sree.s@example.com",
                    Address = new List<Address>(){new ()
                    {
                        AddressID = 3,
                        EmpId = 3,
                        Type = AddressType.Office,
                        Line1 = "456 Business Rd",
                        Line2 = "Suite 101",
                        Line3 = "",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PinCode = 90001
                    } },
                    AddressProof = new List<AddressProof>(){ new AddressProof()
                    {
                        ID = 3,
                        EmpId = 3,
                        Type = AddressProofType.Passport,
                        DocumentNumber = "P123456789"
                    } },
                    PhoneNumber = 123456789
                },
                new Employee
                {
                    EmpId = 4,
                    Name = new List<Name>(){new() { Id = 4,EmpId = 4, First = "vani", Middle = "A", Last = "V" } },
                    EmailId = "vani.V@example.com",
                    Address = new List<Address>(){new ()
                    {
                        AddressID = 4,
                        EmpId = 4,
                        Type = AddressType.Office,
                        Line1 = "456 Business Rd",
                        Line2 = "Suite 101",
                        Line3 = "",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PinCode = 90001
                    } },
                    AddressProof = new List<AddressProof>(){ new AddressProof()
                    {
                        ID = 4,
                        EmpId = 4,
                        Type = AddressProofType.Passport,
                        DocumentNumber = "P123456789"
                    } },
                    PhoneNumber = 123456789
                },
                new Employee
                {
                    EmpId = 5,
                    Name = new List<Name>(){new() { Id= 5, EmpId = 5, First = "John", Middle = "M", Last = "ww" } },
                    EmailId = "john.ww@example.com",
                    Address = new List<Address>(){new()
                    {
                        AddressID = 5,
                        EmpId = 5,
                        Type = AddressType.Home,
                        Line1 = "123 Main St",
                        Line2 = "Apt 4B",
                        Line3 = "",
                        City = "New York",
                        State = "NY",
                        Country = "USA",
                        PinCode = 10001
                    } },
                    AddressProof = new List<AddressProof>(){new ()
                    {
                        ID = 5,
                        EmpId= 5,
                        Type = AddressProofType.DL,
                        DocumentNumber = "DL1234567890"
                    } },
                    PhoneNumber = 1234567890
                },
                new Employee
                {
                    EmpId = 6,
                    Name = new List<Name>(){new() { Id = 6,EmpId = 6, First = "Jane", Middle = "A", Last = "Smith" } },
                    EmailId = "jane.smith@example.com",
                    Address = new List<Address>(){new ()
                    {
                        AddressID = 6,
                        EmpId = 6,
                        Type = AddressType.Office,
                        Line1 = "456 Business Rd",
                        Line2 = "Suite 101",
                        Line3 = "",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PinCode = 90001
                    } },
                    AddressProof = new List<AddressProof>(){ new AddressProof()
                    {
                        ID = 6,
                        EmpId = 6,
                        Type = AddressProofType.Passport,
                        DocumentNumber = "P123456789"
                    } },
                    PhoneNumber = 123456789
                },
                new Employee
                {
                    EmpId = 7,
                    Name = new List<Name>(){new() { Id = 7,EmpId = 7, First = "Jane", Middle = "A", Last = "Smith" } },
                    EmailId = "jane.smith@example.com",
                    Address = new List<Address>(){new ()
                    {
                        AddressID = 7,
                        EmpId = 7,
                        Type = AddressType.Office,
                        Line1 = "456 Business Rd",
                        Line2 = "Suite 101",
                        Line3 = "",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PinCode = 90001
                    } },
                    AddressProof = new List<AddressProof>(){ new AddressProof()
                    {
                        ID = 7,
                        EmpId = 7,
                        Type = AddressProofType.Passport,
                        DocumentNumber = "P123456789"
                    } },
                    PhoneNumber = 123456789
                },
                new Employee
                {
                    EmpId = 8,
                    Name = new List<Name>(){new() { Id= 8, EmpId = 8, First = "John", Middle = "M", Last = "Doe" } },
                    EmailId = "john.doe@example.com",
                    Address = new List<Address>(){new()
                    {
                        AddressID = 8,
                        EmpId = 8,
                        Type = AddressType.Home,
                        Line1 = "123 Main St",
                        Line2 = "Apt 4B",
                        Line3 = "",
                        City = "New York",
                        State = "NY",
                        Country = "USA",
                        PinCode = 10001
                    } },
                    AddressProof = new List<AddressProof>(){new ()
                    {
                        ID = 8,
                        EmpId= 8,
                        Type = AddressProofType.DL,
                        DocumentNumber = "DL1234567890"
                    } },
                    PhoneNumber = 1234567890
                },
                new Employee
                {
                    EmpId = 9,
                    Name = new List<Name>(){new() { Id = 9,EmpId = 9, First = "Jane", Middle = "A", Last = "Smith" } },
                    EmailId = "jane.smith@example.com",
                    Address = new List<Address>(){new ()
                    {
                        AddressID = 9,
                        EmpId = 9,
                        Type = AddressType.Office,
                        Line1 = "456 Business Rd",
                        Line2 = "Suite 101",
                        Line3 = "",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PinCode = 90001
                    } },
                    AddressProof = new List<AddressProof>(){ new AddressProof()
                    {
                        ID = 9,
                        EmpId = 9,
                        Type = AddressProofType.Passport,
                        DocumentNumber = "P123456789"
                    } },
                    PhoneNumber = 123456789
                },
                new Employee
                {
                    EmpId = 10,
                    Name = new List<Name>(){new() { Id = 10,EmpId = 10, First = "Jane", Middle = "A", Last = "Smith" } },
                    EmailId = "jane.smith@example.com",
                    Address = new List<Address>(){new ()
                    {
                        AddressID = 10,
                        EmpId = 10,
                        Type = AddressType.Office,
                        Line1 = "456 Business Rd",
                        Line2 = "Suite 101",
                        Line3 = "",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PinCode = 90001
                    } },
                    AddressProof = new List<AddressProof>(){ new AddressProof()
                    {
                        ID = 10,
                        EmpId = 10,
                        Type = AddressProofType.Passport,
                        DocumentNumber = "P123456789"
                    } },
                    PhoneNumber = 123456789
                }
                // Add 8 more employees to meet the minimum requirement of 10
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}
