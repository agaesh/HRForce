using Microsoft.AspNetCore.Mvc;
using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Helpers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    // GET: api/DepartmentDTO

    [HttpGet]
    public async Task<ActionResult<PagedResult<DepartmentDTO>>> GetDepartments(
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10, [FromQuery] string search = null)
    { 
        // 1. Basic Validation
        if (pageNumber < 1 || pageSize < 1)
        {
            return BadRequest("Page numbers and sizes must be greater than 0.");
        }

        // 2. Cap the page size to prevent memory exhaustion
        const int maxPageSize = 50;
        int actualPageSize = Math.Min(pageSize, maxPageSize);

        // 3. Service call (Service should handle the Mapping/Projection)
        var departments = await _departmentService.GetAllDepartmentsQueryable(pageNumber, actualPageSize, search);

        return Ok(departments);
    }

    // GET: api/DepartmentDTO/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDTO>> GetDepartmentByID(int id)
    {
        var department = await _departmentService.GetDepartmentByIdAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartmentsByStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return BadRequest("Status is required.");

        var departments = await _departmentService.GetDepartmentsByStatusAsync(status);

        return Ok(departments);
    }

    // PUT: api/DepartmentDTO/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, UpdateDepartmentDTO departmentdto)
    {
        try
        {
            var updatedDepartment = await _departmentService
                .UpdateDepartmentAsync(id, departmentdto);

            return Ok(new
            {
                success = true,
                message = "Department updated successfully.",
                data = updatedDepartment
            });
        }

        catch (Exception ex)
        {
            return NotFound(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    // POST: api/DepartmentDTO
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostDepartment(CreateDepartmentDto departmentdto)
    {
        try
        {
            var createdDepartment = await _departmentService.CreateDepartmentAsync(departmentdto);

            return Ok(new
            {
                success = true,
                message = "Department created successfully",
                data = createdDepartment
            });
        }
        catch (Exception ex)
    {
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred while creating the department.",
                error = ex.Message
            });
        }
    }

    // DELETE: api/DepartmentDTO/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        try
        {
            await _departmentService.DeleteDepartmentAsync(id);

            return Ok(new
            {
                success = true,
                message = "Department deleted successfully.",
                deletedDepartmentId = id
            });
        }
        catch ( KeyNotFoundException ex)
        {
            return NotFound(new
            {
                success = false,
                message = ex.Message
            });
        }
        
    }
}
