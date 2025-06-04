namespace OnePieceAPI.Exceptions.Common
{
    public abstract class BaseApiException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        protected BaseApiException(int statusCode, string errorCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        protected BaseApiException(int statusCode, string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
