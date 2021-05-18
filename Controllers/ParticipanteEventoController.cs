using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using TCC.Interfaces;
using TCC.Models;


namespace TCC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("ParticipanteEvento")]
    public class ParticipanteEventoController : ControllerBase
    {
        private readonly IParticipante_evento _participante_Evento;

        public ParticipanteEventoController(IParticipante_evento participante_Evento)
        {
            _participante_Evento = participante_Evento;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CadastrarAsync([Required][FromBody] Participante_evento pe)
        {
            var resultado = await _participante_Evento.CadastrarAsync(pe);

            try
            {
                if (resultado)
                {
                    return StatusCode(HttpStatusCode.Created.GetHashCode());
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "O usuário ja esta cadastrado nesse evento.");
                }
            }
            catch(Exception e)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro inesperado. Entre em contato com o administrador.");
            }
            throw new NotImplementedException();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{idUsuario}/{idEvento}")]
        public async Task<IActionResult> BuscarPeloUsuarioAsync(int idUsuario, int idEvento)
        {
            var resultado = await _participante_Evento.BuscarPeloUsuarioAsync(idUsuario, idEvento);
            try
            {
                if (resultado != null)
                    return Ok(resultado);
                else
                    return NotFound();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{idUsuario}/{idEvento}/Excluir")]
        public async Task<IActionResult> DeletarAsync(int idUsuario, int idEvento)
        {
            var resultado = await _participante_Evento.DeletarAsync(idUsuario, idEvento);
            try
            {
                if (resultado)
                    return StatusCode(HttpStatusCode.OK.GetHashCode());
                else
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Não foi excluido.");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }
    }
}
