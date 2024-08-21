using Microsoft.AspNetCore.Mvc;
using AdminGestaoApi.Infra.CrossCutting.Logging.Dto;
using AdminGestaoApi.ApplicationService.Requests.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces;
using AdminGestaoApi.ApplicationService.Services.Interfaces.Usuario;

namespace AdminGestaoApi.Controllers.Login
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILoggingProvider _logger;
        private readonly IUsuarioApplicationService _usuarioService;

        public AuthController(ILoggingProvider logger, IUsuarioApplicationService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioRequestLogin usuario)
        {
            try
            {
                _logger.Informacao(new LogDto($"Requisição para login do usuario: {usuario.Username}"));

                string? token = await _usuarioService.Login(usuario.Username, usuario.Password);
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.Erro(new LogDto($"Erro ao logar usuario: {ex.Message}"));
                return StatusCode(500);
            }
        }
    }
}
