using kanban_api.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using kanban_api.Utils.GenerateToken;
using kanban_api.Models;
using kanban_api.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kanban_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginBL _loginBL;
        private readonly TokenConfigurations _tokenConfiguration;

        public LoginController(LoginBL loginBL, TokenConfigurations tokenConfiguration)
        {
            _loginBL = loginBL;
            _tokenConfiguration = tokenConfiguration;
        }

        // POST <LoginController>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Login userData)
        {
            _loginBL.ValidateParameters(userData.Username, userData.Password);

            var generateToken = new GenerateToken(_tokenConfiguration);
            string accessToken = generateToken.GenerateJWT(userData.Username, userData.Password);

            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("AccessToken", accessToken);

            return Ok(response);
        }
    }
}
