using EmployeeManagement.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/Employee")]
    [ApiController]
    [ApiVersion("2.0")]
    public class EmployeeV2Controller : ControllerBase
    {
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
        public async Task<ActionResult> UpdateEmployeeV2(Guid empId, Employee emp)
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
