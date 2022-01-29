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
        /// <summary>
        /// Realizar Login
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     Post /api/v1/auth/signin
        ///   
        ///        {
        ///        "login": "string",
        ///        "password": "string"
        ///        }
        ///
        /// </remarks>
        /// <returns>Retorna informações de autenticação</returns>
        /// <response code="200">Usuário fez login com sucesso</response>
        /// <response code="401">Informações do usuário incorreto</response>
        [HttpPost("signin")]
        public ActionResult<TokenVO> Signin([FromBody] UserVO user)
        {
            if(user == null) return BadRequest("Invalid user request");
            var token = _loginBusiness.ValidateCredentials(user);
            if(token == null) return Unauthorized();
            return Ok(token);
        }
        /// <summary>
        /// Renovar token de acesso
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     Post /api/v1/auth/refresh
        ///   
        ///        {
        ///        "acessToken": "string",
        ///        "refreshToken": "string"
        ///        }
        ///
        /// </remarks>
        /// <returns>Retorna token de acesso renovado</returns>
        /// <response code="200">Renovação do token realizado com sucesso</response>
        /// <response code="400">Retorna que o token esta inválido</response>
        [HttpPost("refresh")]
        public ActionResult<TokenVO> Refresh([FromBody] TokenRefreshVO tokenVO)
        {
            if(tokenVO == null) return BadRequest("Invalid token request");
            var token = _loginBusiness.ValidateCredentials(tokenVO);
            if(token == null) return BadRequest("Invalid token request");
            return Ok(token);
        }
        /// <summary>
        /// Realizar logout
        /// </summary>
        /// <remarks>
        /// Exemplo:
        /// 
        ///     Patch /api/v1/auth/revoke
        ///
        /// </remarks>
        /// <response code="200">Retorna que o logout foi feito com sucesso</response>
        /// <response code="400">Retorna que requisão de logout não pode ser realizada</response>
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
