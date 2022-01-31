namespace ProiectDAW.Services.Types
{
    public class AuthenticationResult
    {
        public bool IsError { get; set; }

        public string Token { get; set; }

        public int ErrorType { get; set; }

        public AuthenticationResult(string token)
        {
            Token = token;
            IsError = false;
            ErrorType = 0;
        }

        public AuthenticationResult(int errorType)
        {
            ErrorType = errorType;
            IsError = true;
        }
    }
}
