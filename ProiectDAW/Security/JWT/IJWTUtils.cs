using ProiectDAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Utilities.JWT
{
    public interface IJWTUtils
    {
        public Guid ValidateJWTToken(string token);

        public string GenerateJWTToken(User user);
    }
}
