using ProiectDAW.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models
{
    public class UserSettings : BaseEntity
    {
        public bool RememberPageNumber { get; set; }

        public User User { get; set; }
    }
}
