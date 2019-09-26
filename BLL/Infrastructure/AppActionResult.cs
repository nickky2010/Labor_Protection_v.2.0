using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
namespace BLL.Infrastructure
{
    public class AppActionResult : IAppActionResult
    {
        int status;
        public AppActionResult()
        {
            ErrorMessages = new List<string>();
        }

        public AppActionResult(int status, IList<string> errorMessages)
        {
            Status = status;
            ErrorMessages = errorMessages;
        }
        public AppActionResult(int status)
        {
            Status = status;
            ErrorMessages = new List<string>();
        }

        public int Status
        {
            get => status;
            set
            {
                if (Enum.IsDefined(typeof(HttpStatusCode), value))
                    status = value;
            }
        }
        public bool IsSuccess { get => (Status >= 200) && (Status <= 299); }
        public IList<string> ErrorMessages { get; set; }
    }
    public class AppActionResult<TDTO> : AppActionResult, IAppActionResult<TDTO>
    {
        public TDTO Data { get; set; }
    }

    internal class AppActionResultToken : AppActionResult<Token> { }

    internal class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
