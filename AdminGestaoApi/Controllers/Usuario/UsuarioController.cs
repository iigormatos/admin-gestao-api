using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AdminGestaoApi.ApplicationService.Requests.Usuario;
using AdminGestaoApi.ApplicationService.Responses.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces;
using AdminGestaoApi.ApplicationService.Services.Interfaces.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Dto;

namespace AdminGestaoApi.Controllers.Usuario
{
    [Authorize]
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILoggingProvider _logger;
        private readonly IUsuarioApplicationService _usuarioService;

        public UsuarioController(ILoggingProvider logger, IUsuarioApplicationService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpPost("usuario")]
        public async Task<ActionResult<UsuarioResponse>> Insert([FromBody] UsuarioRequestInsercao usuario)
        {
            try
            {
                _logger.Informacao(new LogDto($"Requisição para inserção de usuário: {JsonConvert.SerializeObject((object)usuario)}"));

                var usuarioInserido = await _usuarioService.Insert(usuario);

                if (usuarioInserido == null)
                {
                    return StatusCode(500);
                }

                return base.CreatedAtAction(nameof(Insert), (object?)usuarioInserido);
            }
            catch (Exception ex)
            {
                _logger.Erro(new LogDto($"Erro ao inserir usuário: {ex.Message}"));
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioResponse>> Get([FromQuery] string username)
        {
            try
            {
                _logger.Informacao(new LogDto($"Requisição para busca de usuário por username: {username}"));
                var usuario = await _usuarioService.GetByUsername(username);

                if (usuario == null)
                {
                    return NotFound();
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.Erro(new LogDto($"Erro ao pesquisar usuário: {ex.Message}"));
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioResponse>> Update([FromBody] UsuarioRequestUpdate usuario)
        {
            try
            {
                _logger.Informacao(new LogDto($"Requisição para update do cliente: {usuario.Username}"));
                var usuarioAtualizado = await _usuarioService.Update(usuario);

                if (usuarioAtualizado == null)
                {
                    return StatusCode(500);
                }

                return Ok(usuarioAtualizado);
            }
            catch (Exception ex)
            {
                _logger.Erro(new LogDto($"Erro ao realizar update no cliente: {ex.Message}"));
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] long id)
        {
            try
            {
                _logger.Informacao(new LogDto($"Requisição para deletar o usuário por ID: {id}"));

                if (await _usuarioService.Delete(id) <= 0)
                {
                    return StatusCode(500);
                }

                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.Erro(new LogDto($"Erro ao realizar deleção no usuário: {ex.Message}"));
                return StatusCode(500);
            }
        }
    }
}
