using Dell.Lead.WeApi.Business;
using Dell.Lead.WeApi.Data.VO;
using Microsoft.AspNetCore.Mvc;

namespace Dell.Lead.WeApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserBusiness _userBusiness;
        
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        /// <summary>
        /// Cadastrar um usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /api/v1/users
        ///   
        ///        {
        ///        "login": "string",
        ///        "password": "string"
        ///        }
        ///
        /// </remarks>
        /// <returns>Retorna o usuário cadastrado</returns>
        /// <response code="201">Usuário criado com sucesso e retorna o usuário criado.</response>
        /// <response code="400">Retorna que falhou a criação do usuário</response>
        [HttpPost]
        public ActionResult<UserVO> Create([FromBody] UserVO user)
        {
            var userEntity = _userBusiness.Create(user);
            if (userEntity == null) return BadRequest("Failed to register the user");
            return CreatedAtAction("FindById", new { id = userEntity.Id}, userEntity);
        }
        /// <summary>
        /// Pesquisar um usuário pelo ID
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET /api/v1/users/1
        ///   
        ///        {
        ///        "login": "string",
        ///        "password": "string"
        ///        }
        ///
        /// </remarks>
        /// <param name="id">ID do funcionário que deseja pesquisar</param>
        /// <returns>Retornar usuário pesquisado pelo ID</returns>
        /// <response code="200">Retorna o usuário pesquisado</response>
        /// <response code="400">Retorna que o usuário não esta cadastrado</response>
        [HttpGet("find-by-id/{id}")]
        public ActionResult<UserVO> FindById(long id)
        {
            var user = _userBusiness.FindById(id);
            if (user == null) return BadRequest("Sem registro de usuário");
            return Ok(user);
        }


    }
}
