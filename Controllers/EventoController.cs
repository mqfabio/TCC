using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using TCC.DTO;
using TCC.Interfaces;
using TCC.Models;

namespace TCC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("evento")]
    public class EventoController : ControllerBase
    {
        private readonly IEvento _evento;

        public EventoController(IEvento evento)
        {
            _evento = evento;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CadastrarAsync([Required][FromBody] Evento evento)
        {
            var resultado = await _evento.CadastrarAsync(evento);

            try
            {
                if (resultado)
                {
                    return StatusCode(HttpStatusCode.Created.GetHashCode());
                }
                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro inesperado. Entre em contato com o administrador.");
            }

            throw new NotImplementedException();
        }


        [HttpPut("id/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AlterarAsync([Required][FromBody] Evento evento)
        {
            var resultado = await _evento.AlterarAsync(evento);
            try
            {
                if (resultado)
                {
                    return StatusCode(HttpStatusCode.Created.GetHashCode());
                }

                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Não foi possivel atualizar o evento.");
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        [HttpDelete("id/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExcluirAsync(int id)
        {
            var resultado = await _evento.ExcluirAsync(id);
            try
            {
                if (resultado)
                {
                    return StatusCode(HttpStatusCode.OK.GetHashCode());
                }

                else
                {
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "O evento não foi excluido.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> PegarPeloNomeAsync(string nome)
        {
            var resultado = await _evento.PegarPeloNomeAsync(nome);
            try
            {
                if (resultado != null)
                {
                    return Ok(resultado);
                }

                else
                {
                    return BadRequest("Insira um nome valido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
            throw new NotImplementedException();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> BuscarTodosAsync()
        {
            var resultado = await _evento.BuscarTodosAsync();
            try
            {
                if (resultado != null)
                    return Ok(resultado);

                else
                    return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("busca")]
        public async Task<ActionResult<List<Evento>>> BuscarEventosPeloNomeouDataTrazendoUsuarioAsync(string nomeEvento, DateTime? dataInicio, DateTime? datafim)
        {
            
            var resultado = await _evento.BuscarEventosPeloNomeouDataTrazendoUsuarioAsync(nomeEvento, dataInicio, datafim);
            try
            {
                if (resultado != null)
                    return Ok(resultado);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        [HttpGet("rm/{rm}")]
        public async Task<IActionResult> BuscarPeloRmAsync(int rm)
        {
            var resultado = await _evento.BuscarPeloRmAsync(rm);
            try
            {
                if(resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound("Insira um RM válido.");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpGet("data/{dataInicio}/{dataFim}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BuscarPeloNomeOuData(DateTime dataInicio, DateTime dataFim)
        {
            var resultado = await _evento.BuscarPorNomeOuData(dataInicio, dataFim);
            try
            {
                if (resultado != null)
                    return Ok(resultado);
                else
                    return NotFound("Evento nao encontrado!");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
