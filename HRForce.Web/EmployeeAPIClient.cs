using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HRForce.Web.DTO;
using HRForce.Web.Helpers;

namespace HRForce.Web
{
    public class EmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Create Employee
        public async Task<PagedResult<EmployeeDTO>> GetEmployeesAsync(int pageNumber = 1, int pageSize = 10, string search = null, string status = null)
        {
            // Base URL
            var url = $"api/Employee?pageNumber={pageNumber}&pageSize={pageSize}";

            // Append search if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                url += $"&search={Uri.EscapeDataString(search)}";
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                url += $"&status={Uri.EscapeDataString(status)}";
            }

            var response = await _httpClient.GetFromJsonAsync<PagedResult<EmployeeDTO>>(url);

            return response ?? new PagedResult<EmployeeDTO>();
        }
        //Create Employee
        public async Task<ApiResponse<EmployeeDTO>> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            var jsonContent = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Employee", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Always deserialize into ApiResponse<EmployeeDTO>
            var result = JsonConvert.DeserializeObject<ApiResponse<EmployeeDTO>>(responseBody)!;

            return result;
        }
        // Update Employee
        public async Task<ApiResponse<EmployeeDTO>> UpdateEmployeeAsync(int id, UpdateEmployeeDTO dto)
        {
            var jsonContent = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Employee/{id}", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ApiResponse<EmployeeDTO>>(responseBody)!;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result?.Message ?? "Update failed");
            }

            return result!;
        }

        // Delete Employee
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Employee/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
