using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.API
{
    public class ServiceResult
    {
        /// <summary>
        /// Messages
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();

        /// <summary>
        /// Result Status
        /// </summary>
        public bool Success { get; set; }
        public bool Failure { get; set; }


        /// <summary>
        /// Error With One Message
        /// </summary>
        /// <param name="errorMessage">ErrorMessage  that will be shown</param>
        public ServiceResult(string errorMessage)
        {
            Success = false;
            Failure = true;
            Messages.Add(errorMessage);
        }

        /// <summary>
        /// Error WithMany Messages
        /// </summary>
        /// <param name="errorMessages">ErrorMessages string List that will be shown</param>
        public ServiceResult(List<string> errorMessages)
        {
            Success = false;
            Failure = true;
            Messages = errorMessages;
        }
        /// <summary>
		/// Success With No Message And ResultObject
		/// </summary>
		public ServiceResult()
        {
            Success = true;
            Failure = false;
        }
    }




    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        /// Result Object
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Success With Result Object and Optional Message
        /// </summary>
        /// <param name="result">Result Object</param>
        /// <param name="successMessage">Otional Message To Show User</param>
        public ServiceResult(T result, string successMessage = "Succeed!")
        {
            Success = true;
            Failure = false;
            Result = result;
            Messages.Add(successMessage);
        }

        public ServiceResult(List<string> messages) : base(messages) { }
        public ServiceResult(string message) : base(message) { }

        public ServiceResult() : base() { }

    }
}
