using HRForce.Web.DTO;
using HRForce.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

public class DepartmentApiClient
{
    private readonly HttpClient _httpClient;

    public DepartmentApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET: api/Department
    public async Task<PagedResult<DepartmentDTO>> GetDepartmentsAsync(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _httpClient.GetFromJsonAsync<PagedResult<DepartmentDTO>>(
            $"api/department?pageNumber={pageNumber}&pageSize={pageSize}"
        );

        return response ?? new PagedResult<DepartmentDTO>();
    }

    // GET: api/Department/5
    public async Task<DepartmentDTO?> GetDepartmentByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/Department/{id}");

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<DepartmentDTO>(json);
    }

    // POST: api/Department
    public async Task<ApiResponse<DepartmentDTO>> CreateDepartmentAsync(CreateDepartmentDto dto)
    {
        var jsonContent = JsonConvert.SerializeObject(dto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/Department", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        // Always deserialize into ApiResponse<DepartmentDTO>
        var result = JsonConvert.DeserializeObject<ApiResponse<DepartmentDTO>>(responseBody)!;

        return result;
    }

    // PUT: api/Department/5
    public async Task<ApiResponse<DepartmentDTO>> UpdateDepartmentAsync(int id, UpdateDepartmentDTO dto)
    {
        try
        {
            var jsonContent = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Department/{id}", content);

            var JSONBody = JObject.Parse(await response.Content.ReadAsStringAsync());

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ApiResponse<DepartmentDTO>>(responseBody);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result?.Message ?? "Update failed");
            }

            return result!;
        
        }
        catch (Exception)
        {
            throw;
        }
    }

    // DELETE: api/Department/5
    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Department/{id}");

        return response.IsSuccessStatusCode;
    }
}