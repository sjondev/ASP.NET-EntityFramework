namespace BlogApi;

public static class Configuration
{
    // TOKEN - JWT - Json Web Token
    public static string JwtKey { get; set; } = "QCNCbG9nSm8qfi9Kb242NCNALShKdW5pb3IlKQ==";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_IlTevUM/z0ey3NwCV/unWg==";
    
    // fiz uma classe para enviar email.
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}