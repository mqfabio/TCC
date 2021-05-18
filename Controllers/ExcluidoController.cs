//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections;
//using System.ComponentModel.DataAnnotations;
//using System.Net;
//using System.Threading.Tasks;
//using TCC.Models;

//namespace TCC.Controllers
//{
//    [ApiController]
//    [Route("usuario")]
//    public class ExcluidoController : ControllerBase
//    {
//        private readonly IExcluido _usuario;

//        public ExcluidoController(IExcluido usuario)
//        {
//            _usuario = usuario;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Criar([Required][FromBody]Excluido usuario)
//        {
//            try
//            {
//                var resultado = await _usuario.CadastrarAsync(usuario);

//                if (resultado)
//                {
//                    return StatusCode(HttpStatusCode.Created.GetHashCode(), "Registro criado com sucesso e o novo ID é: xxxxx");
//                }
//                else
//                {
//                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro inesperado. Entre em contato com o administrador.");
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> BuscarTodosAsync()
//        {
//            var resultado = await _usuario.BuscarTodos();

//            try
//            {
//                if(resultado != null)
//                {
//                    return Ok(resultado);
//                }

//                else
//                {
//                    return BadRequest();
//                }
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//    }
//}
