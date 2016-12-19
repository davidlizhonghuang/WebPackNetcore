namespace WebAppIdenty
{
    public class AppSettings
    {
        public AuthKeys AuthKeys { get; set; }
    }

    public class AuthKeys
    {
        public string AdminPolicyKey { get; set; }
        public string Auth { get; set; }
        public string Web { get; set; }
        public string SuperSecureLogin { get; set; }
    }
}