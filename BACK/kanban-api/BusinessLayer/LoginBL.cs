using kanban_api.Utils;

namespace kanban_api.BusinessLayer
{
    public class LoginBL
    {
        public void ValidateParameters(string username, string password)
        {
            var validateModel = ValidateModel.Start();

            validateModel.Fail(string.IsNullOrWhiteSpace(username), "Username deve ser preenchido.")
                .Fail(string.IsNullOrWhiteSpace(password), "Senha deve ser preenchida.")
                .Validate(StatusCodes.Status400BadRequest);
        }
    }
}
