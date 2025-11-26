using Biblioteca.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Dtos;

namespace Biblioteca.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager,
             TokenService tokenService

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto
            };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Usuário criado com sucesso.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Senha, false);

            if (!result.Succeeded)
                return Unauthorized("Senha inválida.");

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
