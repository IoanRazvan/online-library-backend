namespace ProiectDAW.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserSettingsDTO UserSettings { get; set; }
    }
}
