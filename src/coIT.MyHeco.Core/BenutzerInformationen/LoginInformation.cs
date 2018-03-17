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

        public string Email { get; private set; }
        public string Passwort { get; private set; }

        public LoginInformation ChangePassword(string newPassword)
        {
            return new LoginInformation(Email, newPassword);
        }
    }
}