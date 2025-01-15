using EmployeeManagement.API.DTO;
using EmployeeManagement.DATA.Services;


namespace EmployeeManagement.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _empRepo;

        public EmployeeService(IEmployeeRepo empRepo)
        {
            _empRepo = empRepo;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                var employees = await _empRepo.GetEmployeesAsync();
                if (employees == null || !employees.Any())
                {
                    return Enumerable.Empty<Employee>();
                }
                var empList = new List<Employee>();
                foreach (var employee in employees)
                {
                    var emp = new Employee { Id = employee.EmpId, EmailId = employee.EmailId ?? string.Empty, PhoneNumber = employee.PhoneNumber };
                    await MapNameAsync(emp);
                    await MapAddressAsync(emp);
                    await MapAddressProofAsync(emp);
                    empList.Add(emp);
                }
                return empList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching employees.", ex);
            }
        }

        public async Task<Employee> GetEmployee(Guid empId)
        {
            try
            {
                var employee = await _empRepo.GetEmployeeAsync(empId);
                if (employee != null)
                {
                    var emp = new Employee();
                    emp.Id = employee.EmpId;
                    emp.EmailId = string.IsNullOrEmpty(employee.EmailId) ? "" : employee.EmailId;
                    emp.PhoneNumber = employee.PhoneNumber;
                    await MapNameAsync(emp);
                    await MapAddressAsync(emp);
                    await MapAddressProofAsync(emp);
                    return emp;
                }
                else
                {
                    return new Employee();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the employee.", ex);
            }
        }

        public async Task<Employee> AddEmployeeAsync(Employee emp)
        {
            if (emp == null)
            {
                return new Employee(); // Return empty Employee object for null input
            }

            try
            {
                var employee = new DATA.Models.Employee();
                employee.EmailId = emp.EmailId;
                employee.PhoneNumber = emp.PhoneNumber;
                await MapNameAsync(emp, employee,true);
                await MapAddressAsync(emp, employee, true);
                await MapAddressProofAsync(emp, employee, true);

                var addedEmployee = await _empRepo.AddEmployeeAsync(employee);

                // Retrieve and return the newly added employee
                if (addedEmployee != null)
                    return await GetEmployee(addedEmployee.EmpId);
                return new Employee();

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the employee.", ex);
            }
        }

        public async Task<ServiceResult> DeleteEmployeeAsync(Guid empId)
        {
            if (empId == Guid.Empty)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Invalid employee ID."
                };
            }
            try
            {
                var result = await _empRepo.DeleteEmployeeAsync(empId);

                if (result)
                {
                    return new ServiceResult
                    {
                        Success = true,
                        Message = $"Employee with ID {empId} deleted successfully."
                    };
                }
                return new ServiceResult
                {
                    Success = false,
                    Message = $"Failed to delete employee with ID {empId}. Employee may not exist."
                };
            }
            catch (Exception ex)
            {

                return new ServiceResult
                {
                    Success = false,
                    Message = "An error occurred while deleting the employee. Please try again."
                };
            }
        }

        public async Task<ServiceResult> UpdateEmployeeAsync(Employee emp)
        {
            if (emp == null)
            {
                return new ServiceResult { Success = false, Message = "Invalid employee data." };
            }
            try
            {
                // Map input to data model
                var employee = new DATA.Models.Employee { EmpId = emp.Id, EmailId = emp.EmailId, PhoneNumber = emp.PhoneNumber };
                await UpdateNameAsync(emp);
                await UpdateAddressAsync(emp);
                await UpdateAddressProofAsync(emp);
                // Persist changes
                await _empRepo.UpdateEmployeeAsync(employee);

                return new ServiceResult
                {
                    Success = true,
                    Message = $"Employee updated successfully with EmpID: {emp.Id}"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "An error occurred while updating the employee. Please try again."
                };
            }
        }
        private async Task UpdateAddressProofAsync(Employee emp)
        {
            // Update address proof details
            var addressProof = await _empRepo.GetAddressProofAsync(emp.Id);
            if (addressProof != null)
            {
                addressProof.Type = emp.AddressProof != null ? (DATA.Enums.AddressProofType)emp.AddressProof.Type : 0;
                addressProof.DocumentNumber = emp.AddressProof?.DocumentNumber ?? string.Empty;
            }
        }
        private async Task UpdateAddressAsync(Employee emp)
        {
            // Update address details
            var address = await _empRepo.GetAddressAsync(emp.Id);
            if (address != null)
            {
                address.Type = emp.Address != null ? (DATA.Enums.AddressType)emp.Address.Type : 0;
                address.Line1 = emp.Address?.Line1 ?? string.Empty;
                address.Line2 = emp.Address?.Line2 ?? string.Empty;
                address.Line3 = emp.Address?.Line3 ?? string.Empty;
                address.City = emp.Address?.City ?? string.Empty;
                address.State = emp.Address?.State ?? string.Empty;
                address.Country = emp.Address?.Country ?? string.Empty;
                address.PinCode = emp.Address?.PinCode ?? 0;
            }
        }
        private async Task UpdateNameAsync(Employee emp)
        {
            // Update name details
            var name = await _empRepo.GetNameAsync(emp.Id);
            if (name != null)
            {
                name.First = emp.Name?.First ?? string.Empty;
                name.Middle = emp.Name?.Middle ?? string.Empty;
                name.Last = emp.Name?.Last ?? string.Empty;
            }
        }

        private async Task MapNameAsync(Employee employee, DATA.Models.Employee? emp = null, bool isAddEmpolyee = false)
        {
            if (isAddEmpolyee && emp != null)
            {
                emp.Name = new DATA.Models.Name
                {
                    First = employee.Name?.First ?? string.Empty,
                    Middle = employee.Name?.Middle ?? string.Empty,
                    Last = employee.Name?.Last ?? string.Empty
                };

            }
            else
            {
                var name = await _empRepo.GetNameAsync(employee.Id);
                employee.Name = new Name
                {
                    First = name?.First ?? string.Empty,
                    Middle = name?.Middle ?? string.Empty,
                    Last = name?.Last ?? string.Empty
                };
            }
        }

        private async Task MapAddressAsync(Employee employee, DATA.Models.Employee? emp = null, bool isAddEmployee = false)
        {
            if (isAddEmployee && emp != null)
            {
                emp.Address = new List<DATA.Models.Address>
                {
                    new DATA.Models.Address
                    {
                        Line1 = employee.Address?.Line1 ?? string.Empty,
                        Line2 = employee.Address?.Line2 ?? string.Empty,
                        Line3 = employee.Address?.Line3 ?? string.Empty,
                        City = employee.Address?.City ?? string.Empty,
                        State = employee.Address?.State ?? string.Empty,
                        Country = employee.Address?.Country ?? string.Empty,
                        PinCode = employee.Address?.PinCode ?? 0,
                        Type = employee.Address != null ? (DATA.Enums.AddressType)employee.Address.Type : 0
                    }
                };
            }
            else
            {
                var address = await _empRepo.GetAddressAsync(employee.Id);
                employee.Address = new Address
                {
                    Type = address != null ? (int)address.Type : 0,
                    Line1 = address?.Line1 ?? string.Empty,
                    Line2 = address?.Line2 ?? string.Empty,
                    Line3 = address?.Line3 ?? string.Empty,
                    State = address?.State ?? string.Empty,
                    City = address?.City ?? string.Empty,
                    Country = address?.Country ?? string.Empty
                };
            }
        }

        private async Task MapAddressProofAsync(Employee employee, DATA.Models.Employee? emp = null, bool isAddEmployee = false)
        {
            if (isAddEmployee && emp != null)
            {
                emp.AddressProof = new List<DATA.Models.AddressProof>
                {
                    new DATA.Models.AddressProof
                    {
                        DocumentNumber = employee.AddressProof?.DocumentNumber ?? string.Empty,
                        Type = employee.AddressProof != null ? (DATA.Enums.AddressProofType)employee.AddressProof.Type : 0
                    }
                };
            }
            else
            {
                var addressProof = await _empRepo.GetAddressProofAsync(employee.Id);
                employee.AddressProof = new AddressProof
                {
                    DocumentNumber = addressProof?.DocumentNumber ?? string.Empty,
                    Type = addressProof != null ? (int)addressProof.Type : 0
                };
            }
        }
    }
}
