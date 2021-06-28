using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using TCC.DTO;
using TCC.Enums;
using TCC.Interfaces;
using TCC.Models;
using TCC.Services;

namespace TCC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;
        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }



        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDoUsuarioDTO>> AutenticaçãoDoUsuarioAsync([FromBody] Usuario model)
        {
            var usuario = await _usuario.PegarPeloEmailSenhaAsync(model.Email, model.Senha);

            if (usuario == null)
                return BadRequest("Usuário ou senha inválidos");

            var token = TokenService.GenerateToken(usuario);
            usuario.Senha = "";
            return new LoginDoUsuarioDTO
            {
                Nome = usuario.NomeUsuario,
                Perfil = usuario.Perfil,
                Token = token
            };
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CadastrarAsync(Usuario usuario)
        {
            var validar = await _usuario.PegarPeloRMAsync(usuario.RM);

            if (validar != null)
                return BadRequest("Não pode cadastrar outro usuário com RM ja cadastrado.");
            else if (!Enum.IsDefined(typeof(PerfilEnum), usuario.Perfil))
                return BadRequest("Perfil de usuário inválido.");
            else if (!Enum.IsDefined(typeof(StatusUsuarioEnum), usuario.StatusUsuario))
                return BadRequest("Status do usuário inválido.");

            var resultado = await _usuario.CadastrarAsync(usuario);
            {
                try
                {
                    if (resultado)
                    {
                        return Ok(usuario);
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Usuario existente!");
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro inesperado. Entre em contato com o administrador.");
                }
                throw new NotImplementedException();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BuscartodosAsync()
        {
            var resultado = await _usuario.BuscarTodosAsync();
            try
            {
                if (resultado != null)
                {
                    return Ok(resultado);
                }

                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("email/{email}")]
        public async Task<IActionResult> PegarPeloEmailAsync(string email)
        {
            var resultado = await _usuario.PegarPeloEmailAsync(email);
            try
            {
                if (resultado != null)
                {
                    return Ok(resultado);
                }

                else
                {
                    return BadRequest("Insira um email valido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("id/{id}")]
        public async Task<IActionResult> AlterarAsync([Required][FromBody] Usuario usuario)
        {
            var resultado = await _usuario.AlterarAsync(usuario);

            try
            {
                if (resultado)
                {
                    return StatusCode(HttpStatusCode.OK.GetHashCode());
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("rm/{rm}")]
        public async Task<IActionResult> PegarPeloRMAsync(int rm)
        {
            var resultado = await _usuario.PegarPeloRMAsync(rm);
            try
            {
                if (resultado != null)
                {
                    return Ok(resultado);
                }

                else
                {
                    return BadRequest("Insira um email valido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> PegarPeloNomeAsync(string nome)
        {
            var resultado = await _usuario.PegarPeloNomeAsync(nome);
            try
            {
                if (resultado != null)
                {
                    return Ok(resultado);
                }

                else
                {
                    return BadRequest("Insira um email valido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("busca")]
        public async Task<IActionResult> PegarPeloRMOuNomeAsync(int rm, string nome)
        {
            var resultado = await _usuario.PegarPeloRMOuNomeAsync(rm, nome);
            try
            {
                if (resultado != null)
                {
                    return Ok(resultado);
                }

                else
                {
                    return BadRequest("Usuario não encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }

        [HttpPut("inativar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InativarAsync(Usuario usuario)
        {
            var resultado = await _usuario.InativarAsync(usuario);
            try
            {
                if(resultado != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return BadRequest("Usuario não encontrado");
                }
            }
            catch (Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }

    }
}



