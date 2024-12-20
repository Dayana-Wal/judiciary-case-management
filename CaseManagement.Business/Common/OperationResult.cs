namespace CaseManagement.Business.Common
{
    public class OperationResult<T>
    {
        public T Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        //public static OperationResult<T> CreateOperationResult()
        //{
        //    return new OperationResult<T>();
        //}
        public static OperationResult<T> Success(T data, string message = "Operation Successful")
        {
            return new OperationResult<T>(){ Status = "Success", Message = message, Data = data};
        }

        public static OperationResult<T> Failed(T data, string message = "Operation Failed")
        {
            return new OperationResult<T>() { Status = "Failed", Message = message, Data = data };
        }

        public static OperationResult<T> ValidationError(T data, string message = "Validation Failed")
        {
            return new OperationResult<T>() { Status = "Validation", Message = message, Data = data };
        }

    }
    public class OperationResult
    {
        public string Status { get; set; }
        public string Message { get; set; }

        //public static OperationResult CreateOperationResult(string status, string message)
        //{
        //    return new OperationResult { Status = status, Message = message };
        //}
        public static OperationResult Success(string message = "Operation Successful")
        {
            return new OperationResult { Status = "Success", Message = message };
        }

        public static OperationResult Failed(string message = "Operation Failed")
        {
            return new OperationResult { Status = "Failed", Message = message };
        }

        public static OperationResult ValidationError(string message = "Validation Failed")
        {
            return new OperationResult { Status = "Validation", Message = message };
        }
    }

    //success, failed, validation - static methods

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

}
