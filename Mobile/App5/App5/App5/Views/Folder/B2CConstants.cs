namespace App5.Views.Folder
{
    public static class B2CConstants
    {
        // Azure AD B2C Coordinates
        public static string Tenant = "meihana.onmicrosoft.com";
        public static string AzureADB2CHostname = "meihana.b2clogin.com";
        public static string ClientID = "1d42c4df-d86b-4e3e-94e9-8fa5f6881496";
        public static string PolicySignUpSignIn = "b2c_1_signupandsignin";
        public static string PolicyEditProfile = "b2c_1_editprofile";
        public static string PolicyResetPassword = "b2c_1_Reset";
        public static string TentantRedirectUrl = "meihana.b2clogin.com";
        public static string[] Scopes = { "https://meihana.onmicrosoft.com/backends/rw.read.only" };

        public static string AuthorityBase = $"https://{AzureADB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignInSignUp = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityPasswordReset = $"{AuthorityBase}{PolicyResetPassword}";
        public static string IOSKeyChainGroup = "com.microsoft.adalcache";
    }
}