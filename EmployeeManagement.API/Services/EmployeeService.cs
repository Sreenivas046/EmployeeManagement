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
                    var name = await _empRepo.GetNameAsync(employee.EmpId);
                    var address = await _empRepo.GetAddressAsync(employee.EmpId);
                    var addressProof = await _empRepo.GetAddressProofAsync(employee.EmpId);
                    var emp = new Employee
                    {
                        Id = employee.EmpId,
                        EmailId = employee.EmailId ?? string.Empty,
                        PhoneNumber = employee.PhoneNumber,
                        Name = new Name
                        {
                            First = name?.First ?? string.Empty,
                            Middle = name?.Middle ?? string.Empty,
                            Last = name?.Last ?? string.Empty
                        },
                        Address = new Address
                        {
                            Type = address != null ? (int)address.Type : 0,
                            Line1 = address?.Line1 ?? string.Empty,
                            Line2 = address?.Line2 ?? string.Empty,
                            Line3 = address?.Line3 ?? string.Empty,
                            State = address?.State ?? string.Empty,
                            City = address?.City ?? string.Empty,
                            Country = address?.Country ?? string.Empty
                        },
                        AddressProof = new AddressProof
                        {
                            DocumentNumber = addressProof?.DocumentNumber ?? string.Empty,
                            Type = addressProof != null ? (int)addressProof.Type : 0
                        }
                    };

                    empList.Add(emp);
                }

                return empList;
            }
            catch (Exception ex)
            {
                // Log the exception for diagnostics
                //_logger.LogError(ex, "Error occurred while fetching employees.");
                throw new ApplicationException("An error occurred while fetching employees.", ex);
            }
        }
        public async Task<Employee> GetEmployee(int empId)
        {
            try
            {
                var employee =  await _empRepo.GetEmployeeAsync(empId);
                if (employee != null)
                {
                    Employee emp = new Employee();
                    emp.Name = new Name();
                    emp.Address = new Address();
                    emp.AddressProof = new AddressProof();

                    var name = _empRepo.GetNameAsync(employee.EmpId).Result;
                    var address = _empRepo.GetAddressAsync(employee.EmpId).Result;
                    var addressProof = _empRepo.GetAddressProofAsync(employee.EmpId).Result;

                    emp.Id = employee.EmpId;
                    emp.EmailId = string.IsNullOrEmpty(employee.EmailId) ? "" : employee.EmailId;
                    emp.PhoneNumber = employee.PhoneNumber;

                    //assigning Name
                    emp.Name.First = string.IsNullOrEmpty(name.First) ? "" : name.First;
                    emp.Name.Middle = string.IsNullOrEmpty(name.Middle) ? "" : name.Middle;
                    emp.Name.Last = string.IsNullOrEmpty(name.Last) ? "" : name.Last;

                    //assigning address
                    emp.Address.Type = (int)address.Type;
                    emp.Address.Line1 = string.IsNullOrEmpty(address.Line1) ? "" : address.Line1;
                    emp.Address.Line2 = string.IsNullOrEmpty(address.Line2) ? "" : address.Line2;
                    emp.Address.State = string.IsNullOrEmpty(address.State) ? "" : address.State;
                    emp.Address.City = string.IsNullOrEmpty(address.City) ? "" : address.City;
                    emp.Address.Country = string.IsNullOrEmpty(address.Country) ? "" : address.Country;

                    //assigning address proof
                    emp.AddressProof.DocumentNumber = string.IsNullOrEmpty(addressProof.DocumentNumber) ? "" : addressProof.DocumentNumber;
                    emp.AddressProof.Type = (int)addressProof.Type;
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
                var employee = new DATA.Models.Employee
                {
                    Name = new List<DATA.Models.Name>
            {
                new DATA.Models.Name
                {
                    First = emp.Name.First,
                    Middle = emp.Name.Middle,
                    Last = emp.Name.Last
                }
            },
                    EmailId = emp.EmailId,
                    Address = new List<DATA.Models.Address>
            {
                new DATA.Models.Address
                {
                    Line1 = emp.Address.Line1,
                    Line2 = emp.Address.Line2,
                    Line3 = emp.Address.Line3,
                    City = emp.Address.City,
                    Country = emp.Address.Country,
                    PinCode = emp.Address.PinCode,
                    State = emp.Address.State
                }
            },
                    AddressProof = new List<DATA.Models.AddressProof>
            {
                new DATA.Models.AddressProof
                {
                    DocumentNumber = emp.AddressProof.DocumentNumber,
                    Type = (DATA.Models.AddressProofType)emp.AddressProof.Type
                }
            },
                    PhoneNumber = emp.PhoneNumber
                };

                var addedEmployee = await _empRepo.AddEmployeeAsync(employee);

                // Retrieve and return the newly added employee
                return await GetEmployee(addedEmployee.EmpId);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                //_logger.LogError(ex, "Error occurred while adding an employee.");
                throw new ApplicationException("An error occurred while adding the employee.", ex);
            }
        }
        public async Task<ServiceResult> DeleteEmployeeAsync(int empId)
        {
            if (empId <= 0)
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
                // Log the exception
               // _logger.LogError(ex, "Error deleting employee with ID: {EmpId}", empId);

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
                var employee = new DATA.Models.Employee
                {
                    EmpId = emp.Id,
                    EmailId = emp.EmailId,
                    PhoneNumber = emp.PhoneNumber
                };

                // Update name details
                var name = await _empRepo.GetNameAsync(emp.Id);
                if (name != null)
                {
                    name.First = emp.Name.First;
                    name.Middle = emp.Name.Middle;
                    name.Last = emp.Name.Last;
                }

                // Update address details
                var address = await _empRepo.GetAddressAsync(emp.Id);
                if (address != null)
                {
                    address.Type = (DATA.Models.AddressType)emp.Address.Type;
                    address.Line1 = emp.Address.Line1;
                    address.Line2 = emp.Address.Line2;
                    address.Line3 = emp.Address.Line3;
                    address.City = emp.Address.City;
                    address.State = emp.Address.State;
                    address.Country = emp.Address.Country;
                    address.PinCode = emp.Address.PinCode;
                }

                // Update address proof details
                var addressProof = await _empRepo.GetAddressProofAsync(emp.Id);
                if (addressProof != null)
                {
                    addressProof.Type = (DATA.Models.AddressProofType)emp.AddressProof.Type;
                    addressProof.DocumentNumber = emp.AddressProof.DocumentNumber;
                }

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
                // Log exception details for diagnostics
                //_logger.LogError(ex, "Error updating employee with EmpID: {EmpId}", emp?.Id);
                return new ServiceResult
                {
                    Success = false,
                    Message = "An error occurred while updating the employee. Please try again."
                };
            }
        }


    }
}
