using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Helpers;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<PagedResult<EmployeeDto>>> GetEmployees(
        [FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10,[FromQuery] string search = null, string status = null)
        {
            // 1. Basic Validation
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page numbers and sizes must be greater than 0.");
            }

            // 2. Cap the page size to prevent memory exhaustion
            const int maxPageSize = 50;
            int actualPageSize = Math.Min(pageSize, maxPageSize);

            // 3. Service call (Service handles mapping/projection)
            var employees = await _employeeService.GetAllEmployeesQueryable(pageNumber, actualPageSize, search, status);

            // 4. Return paged result
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
            try
            {
                var createdEmployee = await _employeeService.CreateEmployeeAsync(dto);


                return Ok(new
                {
                    success = true,
                    message = "Department created successfully",
                    data = createdEmployee
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while creating the Employee.",
                    error = ex.Message
                });
            }
         
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, UpdateEmployeeDto dto)
        {
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