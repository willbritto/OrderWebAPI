namespace OrderWebAPI.Services.Response
{
    public class ResponseAPI<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }


        public static ResponseAPI<T> OK(T data, string message = "") => new() { Success = true, Data = data, Message = message };
        public static ResponseAPI<T> Fail(string message) => new() { Success = false, Message = message };
    }
}
