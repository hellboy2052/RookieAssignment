namespace ShareVM
{
    public class ResultVm<T>
    {
        public bool IsSuccess { get; set; }

        public T Value { get; set; }

        public string Error { get; set; }
        
        public static ResultVm<T> Success(T Value) => new ResultVm<T> {IsSuccess = true, Value = Value};

        public static ResultVm<T> Failure(string error) => new ResultVm<T> {IsSuccess = false, Error = error};

    }
}