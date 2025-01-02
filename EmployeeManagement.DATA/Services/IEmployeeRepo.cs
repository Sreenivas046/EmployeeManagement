using EmployeeManagement.DATA.Models;

namespace EmployeeManagement.DATA.Services
{
    public interface IEmployeeRepo
    {
        public Task<IEnumerable<Employee>> GetEmployeesAsync();
        public Task<Employee?> GetEmployeeAsync(int empId);
        public Task<Employee?> AddEmployeeAsync(Employee emp);
        public Task<bool> DeleteEmployeeAsync(int empId);
        public Task UpdateEmployeeAsync(Employee emp);
        public Task<Name?> GetNameAsync(int empId);
        public Task<Address?> GetAddressAsync(int empId);
        public Task<AddressProof?> GetAddressProofAsync(int empId);
    }
}
