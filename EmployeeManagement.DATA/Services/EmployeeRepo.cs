using EmployeeManagement.DATA.Data;
using EmployeeManagement.DATA.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DATA.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeRepo(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Address?> GetAddressAsync(Guid empId)
        {
            if (empId == Guid.Empty)
            {
                throw new ArgumentException("Employee ID must be a valid GUID.", nameof(empId));
            }

            try
            {
                if(await _dbContext.Address.FirstOrDefaultAsync(x => x.Employee.EmpId == empId) == null)
                {
                    return new Address();
                }
                return await _dbContext.Address.FirstOrDefaultAsync(x => x.Employee.EmpId == empId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the address.", ex);
            }
        }

        public async Task<AddressProof?> GetAddressProofAsync(Guid empId)
        {
            if (empId == Guid.Empty)
            {
                throw new ArgumentException("Employee ID must be a valid GUID.", nameof(empId));
            }

            try
            {
                return await _dbContext.AddressProofs.FirstOrDefaultAsync(x => x.Employee.EmpId == empId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the address proof.", ex);
            }
        }


        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                var employees = await _dbContext.Employees.ToListAsync();

                if (employees == null || !employees.Any())
                {
                    return Enumerable.Empty<Employee>();
                }

                return employees;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving employees.", ex);
            }
        }

        public async Task<Employee?> GetEmployeeAsync(Guid empId)
        {
            if (empId == Guid.Empty)
            {
                throw new ArgumentException("Employee ID must be a valid GUID.", nameof(empId));
            }
            try
            {
                return await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmpId == empId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the employee.", ex);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(Guid empId)
        {
            if (empId == Guid.Empty)
            {
                throw new ArgumentException("Employee ID must be a valid GUID.", nameof(empId));
            }

            try
            {
                var employee = await GetEmployeeAsync(empId);
                if (employee == null)
                {
                    return false; // Employee not found
                }

                _dbContext.Employees.Remove(employee);

                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return true; // Deletion successful
                }

                return false; // No changes were saved
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the employee.", ex);
            }
        }


        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee object cannot be null.");
            }

            try
            {
                _dbContext.Entry(employee).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (await GetEmployeeAsync(employee.EmpId) == null)
                {
                    throw new KeyNotFoundException($"Employee with ID {employee.EmpId} not found.", ex);
                }

                throw new ApplicationException("An error occurred while updating the employee.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while updating the employee.", ex);
            }
        }

        public async Task<Employee?> AddEmployeeAsync(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException(nameof(emp), "Employee object cannot be null.");
            }

            try
            {
                await _dbContext.Employees.AddAsync(emp);
                await _dbContext.SaveChangesAsync();

                return await GetEmployeeAsync(emp.EmpId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the employee.", ex);
            }
        }

        public async Task<Name?> GetNameAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID must be a valid GUID.", nameof(id));
            }

            try
            {
                var name = await _dbContext.Names.FirstOrDefaultAsync(x => x.EmpId == id);

                if (name == null)
                {
                    return new Name();
                }

                return name;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the name.", ex);
            }
        }

    }
}
