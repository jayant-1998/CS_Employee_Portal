namespace Employee_Portal.Models.ResponseViewModels
{
    public class ApiViewModel
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int Code { get; set; }
        public string? Message { get; set; }
        public object? Body { get; set; }
    }
}
