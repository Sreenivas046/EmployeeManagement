using EmployeeManagement.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    [ApiVersion("2.0")]
    public class EmployeesHeaderVersioningV2Controller : ControllerBase
    {
        #region 'Version 2' Apis are in progress
        [HttpGet]
        [Route("GetEmployees")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesV2()
        {
            try
            {
                ////var employees = await _empRepo.GetEmployeesAsync();
                //if (employees == null)
                //{
                //    return NoContent();
                //}
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
                //var employee = await _empRepo.AddEmployeeAsync(emp);
                //if (employee == null)
                //{
                //    return BadRequest("Unfortunatly, Employee not get created.");
                //}
                //return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
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
                //var result = await _empRepo.UpdateEmployeeAsync(emp);
                //if (!result.Success)
                //{
                //    return NotFound(result.Message);
                //}
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
        public async Task<ActionResult> DeleteEmployeeV2(Guid empId)
        {
            try
            {
                if (empId != Guid.Empty)
                {
                    //bool result = await _empRepo.DeleteEmployeeAsync(empId);
                    //if (!result)
                    //{
                    //    return NotFound($"Employee with ID {empId} not found.");

                    //}
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
