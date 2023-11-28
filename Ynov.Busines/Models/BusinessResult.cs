namespace Ynov.Business.Models
{
    public class BusinessResult
    {
        public bool IsSuccess { get; set; }

        public BusinessError? Error { get; set; }

        public BusinessResult() { }

        protected BusinessResult(bool isSuccess, BusinessError? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static BusinessResult FromSuccess()
        {
            return new BusinessResult(true, null);
        }

        public static BusinessResult FromError(string errorMessage, BusinessErrorReason reason)
        {
            BusinessError error = new(errorMessage, reason);
            return new BusinessResult(false, error);
        }
    }

    public class BusinessResult<T> : BusinessResult
    {
        public T? Result { get; set; }

        public BusinessResult(bool isSuccess, BusinessError? error, T? result = default) : base(isSuccess, error)
        {
            Result = result;
        }

        public static BusinessResult<T> FromSuccess(T? result)
        {
            return new BusinessResult<T>(true, null, result);
        }

        public static BusinessResult<T> FromError(string errorMessage, BusinessErrorReason reason, T? result = default)
        {
            BusinessError error = new(errorMessage, reason);
            return new BusinessResult<T>(false, error, result);
        }
    }

    public class BusinessError
    {
        public string ErrorMessage { get; set; }

        public BusinessErrorReason Reason { get; set; }

        public BusinessError(string errorMessage, BusinessErrorReason reason)
        {
            ErrorMessage = errorMessage;
            Reason = reason;
        }
    }
    
    public enum BusinessErrorReason
    {
        BusinessRule = 400,
        NotFound = 404,
        InternalServerError = 501,
        DbConflict = 409,
        Forbidden = 403
    }
}