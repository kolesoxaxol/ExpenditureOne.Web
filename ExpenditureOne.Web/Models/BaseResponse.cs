using ExpenditureOne.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenditureOne.Web.Models
{
    public class BaseResponse
    {
        public ErrorCodes Code { get; set; } = ErrorCodes.Success;

        public string Message { get; set; }


    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
