using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Data.VO;

namespace RestWithASPNETErudio.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null)
                return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null)
                return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if (token == null)
                return BadRequest("Invalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User?.Identity?.Name; 
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid client request");

            var result = _loginBusiness.RevokeToken(username);

            if (!result)
                return BadRequest("Invalid client request");
            return NoContent();
        }
    }
}
