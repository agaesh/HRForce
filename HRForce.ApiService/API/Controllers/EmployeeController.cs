using Microsoft.AspNetCore.Mvc;
using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Application.Interfaces;

namespace HRForce.ApiService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(CreateEmployeeDto dto)
        {
            var createdEmployee = await _employeeService.CreateEmployeeAsync(dto);

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = createdEmployee.Id },
                createdEmployee
            );
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, UpdateEmployeeDto dto)
        {
            //purposefully left with success mesage and having not return obj
            if (id != dto.Id)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Employee ID mismatch."
                });
            }

            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Employee updated successfully.",
                    data = updatedEmployee
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);

                return Ok(new
                {
                    success = true,
                    message = "Employee deleted successfully.",
                    deletedEmployeeId = id
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}