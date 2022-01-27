using Dell.Lead.WeApi.Business;
using Dell.Lead.WeApi.Data.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dell.Lead.WeApi.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost("signin")]
        public ActionResult<TokenVO> Signin([FromBody] UserVO user)
        {
            if(user == null) return BadRequest("Invalid user request");
            var token = _loginBusiness.ValidateCredentials(user);
            if(token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost("refresh")]
        public ActionResult<TokenVO> Refresh([FromBody] TokenRefreshVO tokenVO)
        {
            if(tokenVO == null) return BadRequest("Invalid token request");
            var token = _loginBusiness.ValidateCredentials(tokenVO);
            if(token == null) return BadRequest("Invalid token request");
            return Ok(token);
        }

        [HttpPatch("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var login = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(login);
            if(!result) return BadRequest("Invalid revoke request");
            return Ok();
        }
    }
}
