using Microsoft.AspNetCore.Mvc;
using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Application.Interfaces;

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

    // PUT: api/DepartmentDTO/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, UpdateDepartmentDTO departmentdto)
    {
        if (id != departmentdto.Id)
        {
            return BadRequest(new
            {
                success = false,
                message = "Department ID mismatch."
            });
        }

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

        catch (KeyNotFoundException ex)
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
    public async Task<ActionResult<DepartmentDTO>> PostDepartment(DepartmentDTO departmentdto)
    {
        var createdDepartment = await _departmentService.CreateDepartmentAsync(
            new CreateDepartmentDto
            {
                DepartmentCode = departmentdto.DepartmentCode,
                DepartmentName = departmentdto.DepartmentName,
                Status = departmentdto.Status,
                CreatedAt = DateTime.UtcNow
            });

        return CreatedAtAction(nameof(GetDepartmentByID), new { id = createdDepartment.Id },createdDepartment);
    }

    // DELETE: api/DepartmentDTO/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {

        await _departmentService.DeleteDepartmentAsync(id);

        return Ok(new
        {
            success = true,
            message = "Department deleted successfully.",
            deletedDepartmentId = id
        });
    }

    private async Task<bool> DepartmentDTOExists(int? id)
    {
        var department = await _departmentService.GetDepartmentByIdAsync(id ?? 0);

        if (department != null)
        {
            return true;
        }

        return false;
    }
}
