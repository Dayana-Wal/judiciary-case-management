namespace CaseManagement.Business.Common
{
    public class OperationResult<T>
    {
        public T Data { get; set; }
        public OperationStatus Status { get; set; }
        public string Message { get; set; }

        public static OperationResult<T> Success(T data, string message = "Operation Successful")
        {
            return new OperationResult<T>(){ Status = OperationStatus.Success, Message = message, Data = data};
        }

        public static OperationResult<T> Failed(T data, string message = "Operation Failed")
        {
            return new OperationResult<T>() { Status = OperationStatus.Failed, Message = message, Data = data };
        }

        public static OperationResult<T> ValidationError(T data, string message = "Validation Failed")
        {
            return new OperationResult<T>() { Status = OperationStatus.Validation, Message = message, Data = data };
        }

    }
    public class OperationResult
    {
        public OperationStatus Status { get; set; }
        public string Message { get; set; }

        public static OperationResult Success(string message = "Operation Successful")
        {
            return new OperationResult { Status = OperationStatus.Success, Message = message };
        }

        public static OperationResult Failed(string message = "Operation Failed")
        {
            return new OperationResult { Status = OperationStatus.Failed, Message = message };
        }

        public static OperationResult ValidationError(string message = "Validation Failed")
        {
            return new OperationResult { Status = OperationStatus.Validation, Message = message };
        }
    }

    public class OperationResultConverter
    {
        public static OperationResult<T> ConvertTo<T>(OperationResult result, T data)
        {
            return new OperationResult<T>
            {
                Status = result.Status,
                Message = result.Message,
                Data = data
            };

        }
    }

    public enum OperationStatus
    {
        Success,
        Failed,
        Validation
    }

}
