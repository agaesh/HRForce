namespace HRForce.Web.DTO
{
    public class ApiResponse<T>
    {
        //Properties appear here written to Standardize api Output
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        //This contain List Of Validation Error 
        public Dictionary<string, List<string>> Errors { get; set; } = new();


        public string Error { get; set; }
    }
}
