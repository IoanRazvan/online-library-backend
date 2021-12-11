using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Services.Types
{
    public class AuthenticationResult
    {
        public string Token {get; set;}

        public bool IsError { get; set; }

        public object Error { get; set; }
    }
}
