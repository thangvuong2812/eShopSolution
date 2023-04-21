using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }
        public ApiErrorResult()
        {

        }

        public ApiErrorResult(string message)
        {
            Message = message;
            IsSucceed = false;
        }

        public ApiErrorResult(string[] validationErrors)
        {
            ValidationErrors = validationErrors;
            IsSucceed = false;
        }
    }
}
