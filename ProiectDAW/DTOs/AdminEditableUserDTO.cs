using System;

namespace ProiectDAW.DTOs
{
    public class AdminEditableUserDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserRole { get; set; }

        public bool IsDisabled { get; set; }
    }
}
