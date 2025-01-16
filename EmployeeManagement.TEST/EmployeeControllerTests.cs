using EmployeeManagement.API.Controllers;
using EmployeeManagement.API.DTO;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.TEST
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _serviceMock;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _serviceMock = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_serviceMock.Object);
        }
        [Fact]
        public async void GetEmployee_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var empId = Guid.NewGuid(); 
            var employee = new Employee
            {
                Id = empId,
                Name = new Name() { First = "Sree", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };
            _serviceMock.Setup(s => s.GetEmployee(empId)).ReturnsAsync(employee);

            // Act
            var result = await _controller.GetEmployee(empId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedProduct = Assert.IsType<Employee>(okResult.Value);
            Assert.Equal(empId, returnedProduct.Id);
        }

        /// <summary>
        /// Unit test case for retuns OK Result, when try to get all employees
        /// </summary>

        [Fact]
        public async void Get_All_Employees_ReturnsOkResult()
        {
            // Arrange
            //var productId = 1;
            var lstproducts = new List<Employee>()
            { new Employee ()
            {
                Id = Guid.NewGuid(),
                Name = new Name() { First = "Sree", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            },
            new Employee ()
            {
                Id = Guid.NewGuid(),
                Name = new Name() { First = "Ravi", Middle = "M", Last = "Ravi" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "ravi.g@gmail.com",
                PhoneNumber = 00019827
            }
            };
            _serviceMock.Setup(s => s.GetEmployeesAsync()).ReturnsAsync(lstproducts);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Employee>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedEmployees = Assert.IsType<List<Employee>>(okResult.Value);

            Assert.Equal(2, returnedEmployees.Count);
            Assert.Equal("Sree", returnedEmployees[0].Name.First);
        }

        /// <summary>
        /// returns no Content result if there is no items in database 
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task GetAllEmployees_ServiceReturnsNull_ReturnsNoContentResult()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetEmployeesAsync()).ReturnsAsync((List<Employee>)null);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Employee>>>(result);
            Assert.IsType<NoContentResult>(actionResult.Result);
        }

        /// <summary>
        /// retuen Created action if the item created succesfully.
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task AddEmployee_ValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var employeeDto = new Employee()
            {
                Name = new Name() { First = "Sree", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };
            var createdEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = new Name() { First = "Sree", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };

            _serviceMock.Setup(s => s.AddEmployeeAsync(employeeDto)).ReturnsAsync(createdEmployee);

            // Act
            var result = await _controller.AddEmployee(employeeDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnedEmployee = Assert.IsType<Employee>(createdResult.Value);

            Assert.Equal(createdEmployee.Id, returnedEmployee.Id);
            Assert.Equal(createdEmployee.Name, returnedEmployee.Name);
        }

        /// <summary>
        /// returns BadRequest if if the item is null, when try to add new item
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task AddEmployee_NullData_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.AddEmployee(null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        /// <summary>
        /// return Badrequest if the service returns null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddEmployee_ServiceReturnsNull_ReturnsBadRequest()
        {
            // Arrange
            var employeeDto = new Employee()
            {
                Name = new Name() { First = "Sree", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };
            _serviceMock.Setup(s => s.AddEmployeeAsync(employeeDto)).ReturnsAsync((Employee?)null);

            // Act
            var result = await _controller.AddEmployee(employeeDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
        /// <summary>
        /// returns Internal server error if there is any exception thrown from the service
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task AddEmployee_ServiceThrowsException_ReturnsInternalServerError()
        {
            var employeeDto = new Employee()
            {
                Name = new Name() { First = "Sree", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };
            _serviceMock.Setup(s => s.AddEmployeeAsync(employeeDto)).ThrowsAsync(new Exception("Database error"));
            var result = await _controller.AddEmployee(employeeDto);

            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);

        }

        /// <summary>
        /// returns OK Object when the item deleted succesfully.
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task DeleteEmployee_ValidId_ReturnsOkObject()
        {
            var employeeId = Guid.NewGuid();
            var ServiceResul = new ServiceResult();
            ServiceResul.Success = true;

            _serviceMock.Setup(s => s.DeleteEmployeeAsync(employeeId)).ReturnsAsync(ServiceResul);

            var result = await _controller.DeleteEmployee(employeeId);

            Assert.IsType<OkObjectResult>(result);
        }

        /// <summary>
        /// return Not found when item ID is not present in database to delete.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteEmployee_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var employeeId = Guid.NewGuid(); // Non-existing employee ID

            var ServiceResul = new ServiceResult();
            ServiceResul.Success = false;
            _serviceMock.Setup(s => s.DeleteEmployeeAsync(employeeId)).ReturnsAsync(ServiceResul);

            // Act
            var result = await _controller.DeleteEmployee(employeeId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Employee with ID {employeeId} not found.", notFoundResult.Value);
        }

        /// <summary>
        /// Throws an exception when there is any exception thrown from the service while deleting an item.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteEmployee_ServiceThrowsException_RetunsInternalServerError()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _serviceMock.Setup(s => s.DeleteEmployeeAsync(employeeId)).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var result = await _controller.DeleteEmployee(employeeId);
            var expResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, expResult.StatusCode);
        }


        //Test Cases for Update api

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var productDto = new Employee()
            {
                Name = new Name() { First = "Updated Name", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };

            // Act
            var result = await _controller.UpdateEmployee(Guid.NewGuid(), productDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID mismatch.", badRequestResult.Value);
        }


        [Fact]
        public async Task Update_ReturnsNotFound_WhenProductNotFound()
        {
            // Arrange
            Guid empId = Guid.NewGuid();
            var productDto = new Employee()
            {
                Id = empId,
                Name = new Name() { First = "Updated Name", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };
            _serviceMock
                .Setup(service => service.UpdateEmployeeAsync(productDto))
                .ReturnsAsync(new ServiceResult { Success = false, Message = "Employee not found" });

            // Act
            var result = await _controller.UpdateEmployee(empId, productDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Employee not found", notFoundResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            Guid empId = Guid.NewGuid();
            var productDto = new Employee()
            {
                Id = empId,
                Name = new Name() { First = "Updated Name", Middle = "M", Last = "Seenu" },
                Address = new Address()
                {
                    Type = 2,
                    Line1 = "Marathahalli",
                    Line2 = "bengalur",
                    Line3 = "bengalur",
                    State = "Karnataka",
                    City = "Bengaluru ",
                    PinCode = 560037,
                    Country = " "
                },
                AddressProof = new AddressProof()
                {
                    Type = 1,
                    DocumentNumber = "OUTYT88926"
                },
                EmailId = "sree.g@gmail.com",
                PhoneNumber = 00019827
            };
            _serviceMock
                .Setup(service => service.UpdateEmployeeAsync(productDto))
                .ReturnsAsync(new ServiceResult { Success = true });

            // Act
            var result = await _controller.UpdateEmployee(empId, productDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }


    }
}