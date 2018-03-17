namespace coIT.MyHeco.Core.BenutzerInformationen
{
    public class LoginInformation
    {
        private LoginInformation()
        {
        }

        public LoginInformation(string email, string passwort)
        {
            Passwort = passwort ?? string.Empty;
            Email = email;
        }

        public string Email { get; }
        public string Passwort { get; }

        public LoginInformation ChangePassword(string newPassword)
        {
            return new LoginInformation(Email, newPassword);
        }
    }
}