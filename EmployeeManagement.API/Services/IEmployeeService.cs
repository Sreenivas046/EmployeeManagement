using System.Collections.Generic;
using EmployeeManagement.API.DTO;

namespace EmployeeManagement.API.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetEmployeesAsync();
        public Task<Employee> GetEmployee(int empId);
        public Task<Employee> AddEmployeeAsync(Employee emp);
        public Task<ServiceResult> DeleteEmployeeAsync(int empId);
        public Task<ServiceResult> UpdateEmployeeAsync(Employee emp);
    }
}
