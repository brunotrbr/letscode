using kanban_api.Authentication;
using kanban_api.Utils;

namespace kanban_api.BusinessLayer
{
    public class LoginBL
    {
        private readonly LoginConfigurations _loginConfiguration;
        public LoginBL(LoginConfigurations loginConfiguration)
        {
            _loginConfiguration = loginConfiguration;
        }
        public void ValidateParameters(string username, string password)
        {
            var validateModel = ValidateModel.Start();

            bool failUsername = string.IsNullOrWhiteSpace(username) || !string.Equals(username, _loginConfiguration.Login, StringComparison.InvariantCultureIgnoreCase);
            bool failPassword = string.IsNullOrWhiteSpace(password) || !string.Equals(password, _loginConfiguration.Senha, StringComparison.InvariantCultureIgnoreCase);
            validateModel.Fail(failUsername, "Login vazio ou inválido.")
                .Fail(failPassword, "Senha vazia ou inválida.")
                .Validate(StatusCodes.Status400BadRequest);
        }
    }
}
;