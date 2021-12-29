using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Services.Types
{
    public class ErrorBody
    {
        public string Message { get; set; }

        public static ErrorBody FromMessage(string message)
        {
            return new ErrorBody { Message = message };
        }
    }
}
