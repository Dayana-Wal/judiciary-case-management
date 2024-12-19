

namespace CaseManagement.Business.Common
{
    public class OperationResult<T>
    {
        public T Data { get; set; }
        public OperationStatus Status { get; set; }
        public string Message { get; set; }
        
    }

        public class OperationResult
        {
        public OperationStatus Status { get; set; }
        public string Message { get; set; }
        }

        public enum OperationStatus
        {
            Success,
            Error,
            Failed
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

}
