using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using HRForce.Web.DTO;

public class DepartmentApiClient
{
    private readonly HttpClient _httpClient;

    public DepartmentApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET: api/Department
    public async Task<List<DepartmentDTO>?> GetDepartmentsAsync()
    {
        var response = await _httpClient.GetAsync("api/Department");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<DepartmentDTO>>(json);
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
    public async Task<DepartmentDTO?> CreateDepartmentAsync(CreateDepartmentDto dto)
    {
        var jsonContent = JsonConvert.SerializeObject(dto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/Department", content);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<DepartmentDTO>(json);
    }

    // PUT: api/Department/5
    public async Task<bool> UpdateDepartmentAsync(int id, UpdateDepartmentDTO dto)
    {
        var jsonContent = JsonConvert.SerializeObject(dto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"api/Department/{id}", content);

        return response.IsSuccessStatusCode;
    }

    // DELETE: api/Department/5
    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Department/{id}");

        return response.IsSuccessStatusCode;
    }
}