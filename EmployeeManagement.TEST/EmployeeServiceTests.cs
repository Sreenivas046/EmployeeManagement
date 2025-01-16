using EmployeeManagement.API.Services;
using EmployeeManagement.DATA.Models;
using EmployeeManagement.DATA.Services;
using Moq;

namespace EmployeeManagement.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepo> _mockRepo;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockRepo = new Mock<IEmployeeRepo>();
            _employeeService = new EmployeeService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetEmployeesAsync_ShouldReturnEmptyList_WhenNoEmployeesExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync((IEnumerable<Employee>)null);

            // Act
            var result = await _employeeService.GetEmployeesAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEmployeesAsync_ShouldReturnEmployees_WhenEmployeesExist()
        {
            // Arrange
            var employees = new List<DATA.Models.Employee>
            {
                new Employee { EmpId = Guid.NewGuid(), EmailId = "test1@example.com", PhoneNumber = 1234567890 },
                new Employee { EmpId = Guid.NewGuid(), EmailId = "test2@example.com", PhoneNumber = 9876543210 }
            };
            _mockRepo.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetEmployeesAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetEmployee_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var empId = Guid.NewGuid();
            var employee = new Employee { EmpId = empId, EmailId = "test@example.com", PhoneNumber = 1234567890 };
            _mockRepo.Setup(repo => repo.GetEmployeeAsync(empId)).ReturnsAsync(employee);

            // Act
            var result = await _employeeService.GetEmployee(empId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(empId, result.Id);
        }

        [Fact]
        public async Task GetEmployee_ShouldReturnEmptyEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var empId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetEmployeeAsync(empId)).ReturnsAsync((Employee)null);

            // Act
            var result = await _employeeService.GetEmployee(empId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldReturnNewEmployee_WhenEmployeeIsAdded()
        {
            // Arrange
            var emp = new API.DTO.Employee { EmailId = "test@example.com", PhoneNumber = 1234567890 };
            var addedEmployee = new Employee { EmpId = Guid.NewGuid(), EmailId = "test@example.com", PhoneNumber = 1234567890 };
            _mockRepo.Setup(repo => repo.AddEmployeeAsync(It.IsAny<Employee>())).ReturnsAsync(addedEmployee);
            _mockRepo.Setup(repo => repo.GetEmployeeAsync(addedEmployee.EmpId)).ReturnsAsync(addedEmployee);

            // Act
            var result = await _employeeService.AddEmployeeAsync(emp);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(addedEmployee.EmpId, result.Id);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldReturnSuccess_WhenEmployeeIsDeleted()
        {
            // Arrange
            var empId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteEmployeeAsync(empId)).ReturnsAsync(true);

            // Act
            var result = await _employeeService.DeleteEmployeeAsync(empId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal($"Employee with ID {empId} deleted successfully.", result.Message);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ShouldReturnFailure_WhenEmployeeIsNotDeleted()
        {
            // Arrange
            var empId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.DeleteEmployeeAsync(empId)).ReturnsAsync(false);

            // Act
            var result = await _employeeService.DeleteEmployeeAsync(empId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal($"Failed to delete employee with ID {empId}. Employee may not exist.", result.Message);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldReturnSuccess_WhenEmployeeIsUpdated()
        {
            // Arrange
            var emp = new API.DTO.Employee { Id = Guid.NewGuid(), EmailId = "test@example.com", PhoneNumber = 1234567890 };
            _mockRepo.Setup(repo => repo.UpdateEmployeeAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeService.UpdateEmployeeAsync(emp);

            // Assert
            Assert.True(result.Success);
            Assert.Equal($"Employee updated successfully with EmpID: {emp.Id}", result.Message);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ShouldReturnFailure_WhenEmployeeIsNull()
        {
            // Act
            var result = await _employeeService.UpdateEmployeeAsync(null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid employee data.", result.Message);
        }
    }

}

