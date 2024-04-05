using Domain.API;

namespace Domain.Base
{
    public class ApiResult
    {
        public bool Success { get; set; }

        /// <summary>
        /// Api Result StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Messages
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceResult"></param>
        public ApiResult(ServiceResult serviceResult)
        {
            Messages = serviceResult.Messages;
            Success = serviceResult.Success;

            if (serviceResult.Failure)
                StatusCode = 400;

            if (serviceResult.Success)
                StatusCode = 200;
        }

        /// <summary>
        /// Api Response With One Error
        /// </summary>
        /// <param name="error">Error Message</param>
        public ApiResult(string error)
        {
            StatusCode = 400;
            Success = false;
            if (error != null)
                Messages.Add(error);
        }

        /// <summary>
        /// Api Response With Many Errors
        /// </summary>
        /// <param name="errors">Error Messages</param>
        public ApiResult(List<string> errors)
        {
            StatusCode = 400;
            Success = false;
            if (errors != null)
                Messages = errors;
        }

        /// <summary>
        /// Initialize Constructor
        /// </summary>
        public ApiResult()
        {

        }

    }



    public class ApiResult<T> : ApiResult
    {

        /// <summary>
        /// Result Object
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Api Result With Object Direct From Service
        /// </summary>
        /// <param name="serviceResult">Result From Service</param>
        public ApiResult(ServiceResult<T> serviceResult) : base(serviceResult)
        {
            Success = serviceResult.Success;
            Result = serviceResult.Result;
        }

        /// <summary>
        /// Api Result With Object Direct From Service With Optional Many Messages
        /// </summary>
        /// <param name="result">Result Object</param>
        /// <param name="messages">Optional Messages</param>
        public ApiResult(T result, List<string> messages = null) : base(messages)
        {
            Success = true;
            Result = result;
        }

        /// <summary>
        /// Api Result With Object Direct From Service With Optional One Message
        /// </summary>
        /// <param name="result">Result Object</param>
        /// <param name="message">Optional Message</param>
        public ApiResult(T result, string message = null) : base(message)
        {
            Success = true;
            Result = result;
        }

        public ApiResult(string message) : base(message) { }
        public ApiResult(List<string> messages) : base(messages) { }
    }
}
