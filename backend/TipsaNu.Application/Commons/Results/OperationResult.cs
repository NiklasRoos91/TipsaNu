namespace TipsaNu.Application.Commons.Results
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public List<string>? ErrorMessages { get; set; }
        public T? Data { get; set; }

        public static OperationResult<T> Success(T value) =>
            new() { IsSuccess = true, Data = value };

        public static OperationResult<T> Failure(string errorMessage) =>
            new() { IsSuccess = false, ErrorMessage = errorMessage };
        public static OperationResult<T> Failure(List<string> errorMessages) =>
            new() { IsSuccess = false, ErrorMessages = errorMessages, ErrorMessage = string.Join(", ", errorMessages) };
    }
}
