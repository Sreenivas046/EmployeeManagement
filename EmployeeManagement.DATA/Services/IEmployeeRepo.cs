using EmployeeManagement.DATA.Models;

namespace EmployeeManagement.DATA.Services
{
    public interface IEmployeeRepo
    {
        public Task<IEnumerable<Employee>> GetEmployeesAsync();
        public Task<Employee?> GetEmployeeAsync(Guid empId);
        public Task<Employee?> AddEmployeeAsync(Employee emp);
        public Task<bool> DeleteEmployeeAsync(Guid empId);
        public Task UpdateEmployeeAsync(Employee emp);
        public Task<Name?> GetNameAsync(Guid empId);
        public Task<Address?> GetAddressAsync(Guid empId);
        public Task<AddressProof?> GetAddressProofAsync(Guid empId);
    }
}
