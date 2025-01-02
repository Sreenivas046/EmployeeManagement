using EmployeeManagement.API.DTO;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _empRepo;

        public EmployeeController(IEmployeeService empRepo)
        {
            _empRepo = empRepo;
        }
        /// <summary>
        /// Creates a response which will contains List of Employees
        /// </summary>
        /// <returns>Returns list of employees in Employee Table </returns>
        [HttpGet]
        [Route("GetEmployees")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees() 
        {
            try
            {
                var employees = await _empRepo.GetEmployeesAsync();
                if (employees == null)
                {
                    return NoContent();
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);         
            }
            
        }

        /// <summary>
        /// Retrieves the details of an employee based on the provided employee ID.
        /// </summary>
        /// <param name="empID">The unique identifier of the employee.</param>
        /// <returns>Returns the details of the employee if found, or a suitable HTTP response.</returns>
        [HttpGet]
        [Route("GetEmployee/{empID:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int empID)
        {
            if (empID <= 0)
            {
                return BadRequest("Invalid Employee ID.");
            }

            var employee = await _empRepo.GetEmployee(empID);

            if (employee == null)
            {
                return NotFound($"Employee with ID {empID} not found.");
            }

            return Ok(employee);
        }


        /// <summary>
        /// Create the response which will have the details of newly created employee and creates the new employee.
        /// </summary>
        /// <param name="emp"> Employee details to create the new employee</param>
        /// <returns>employee which has created newly.</returns>
        [HttpPost]
        [Route("AddEmployee")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee emp) 
        {
            try
            {
                if (emp == null)
                {
                    return BadRequest("Employee data is required.");
                }
                var employee = await _empRepo.AddEmployeeAsync(emp);
                if(employee == null)
                {
                    return BadRequest("Unfortunatly, Employee not get created.");
                }
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            
        }

        /// <summary>
        /// Delete the existing employee by accepting the employee id.
        /// </summary>
        /// <param name="empId"> Employee ID to delete the existing Employee</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteEmployee/id")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> DeleteEmployee(int empId)
        {
            try
            {
                if(empId != 0)
                {
                    var result = await _empRepo.DeleteEmployeeAsync(empId);
                    if (!result.Success)
                    {
                        return NotFound($"Employee with ID {empId} not found.");
                        
                    }
                    return Ok("Employee Deleted Succesfully");
                }
                return BadRequest($"Employee ID should not be zero.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }

        /// <summary>
        /// Update the existing employee by accepting id and the new changes
        /// </summary>
        /// <param name="empId"> Employee ID to update the existing employee</param>
        /// <param name="emp">Employee object with new changes </param>
        /// <returns>retuns the OK result if api gets success and Badrequest if any failures.</returns>
        [HttpPut]
        [Route("UpdateEmployee/id")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> UpdateEmployee(int empId, Employee emp)
        {
            try
            {

                if (empId != emp.Id)
                {
                    return BadRequest("ID mismatch.");
                }
                var result = await _empRepo.UpdateEmployeeAsync(emp);
                if (!result.Success)
                {
                    return NotFound(result.Message);
                }
                return Ok(result);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        //---------------------------------------------------------------------------------------------------------

        #region  'Version 2' Apis are in progress
        [HttpGet]
        [Route("GetEmployees")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesV2()
        {
            try
            {

                await Task.CompletedTask;
                return Ok("List of Employees, Version 2 is in progerss");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("AddEmployee")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<Employee>> AddEmployeeV2([FromBody] Employee emp)
        {
            try
            {
                if (emp == null)
                {
                    return BadRequest("Employee data is required.");
                }
                await Task.CompletedTask;   
                return Ok("Add Employee Version 2 is inprogress");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }

        [HttpPut]
        [Route("UpdateEmployee/id")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult> UpdateEmployeeV2(int empId, Employee emp)
        {
            try
            {

                if (empId != emp.Id)
                {
                    return BadRequest("ID mismatch.");
                }
                await Task.CompletedTask;
                return Ok("Update Employee Version 2 is inprogress");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("DeleteEmployee/id")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult> DeleteEmployeeV2(int empId)
        {
            try
            {
                if (empId != 0)
                {
                    await Task.CompletedTask;
                    return Ok("Delete Employee Version 2 is inprogress");
                }
                return BadRequest($"Employee ID should not be zero.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }
        #endregion
    }
}
