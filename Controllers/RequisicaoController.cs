using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("Requisicao")]
    public class RequisicaoController : ControllerBase
    {
        private readonly IRequisicao _requisicao;

        public RequisicaoController(IRequisicao requisicao)
        {
            _requisicao = requisicao;
        }


        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var resultado = await _requisicao.BuscarTodos();
            try
            {
                if (resultado != null)
                    return Ok(resultado);

                else
                    return BadRequest();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CadastrarAsycn([Required][FromBody] Requisicao requisicao)
        {
            var resultado = await _requisicao.CadastrarAsync(requisicao);

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

        [HttpGet("{email}")]
        public async Task<IActionResult> BuscarPeloEmail(string email)
        {
            var resultado = await _requisicao.BuscarPorEmail(email);
            try
            {
                if (resultado != null)
                    return Ok(resultado);
                else
                    return BadRequest();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }





        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> CadastrarAsync(Requisicao requisicao)
        //{
        //    var imagemNome = Guid.NewGuid() + "_" + requisicao.Pathanexodocumento;
        //    if(!UploadArquivo(requisicao.Pathanexodocumento, imagemNome))
        //    {
        //        return null;
        //    }

        //    requisicao.Pathanexodocumento = imagemNome;

        //    var resultado = await _requisicao.CadastrarAsync(requisicao);
        //    try
        //    {
        //        if(resultado)
        //            return StatusCode(HttpStatusCode.Created.GetHashCode());
        //        else
        //            return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
        //    }
        //    catch
        //    {
        //        return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro inesperado. Entre em contato com o administrador.");
        //    }
        //    throw new NotImplementedException();
        //}

        //public bool UploadArquivo(string arquivo, string imgNome)
        //{
        //    var imageDataByteArray = Convert.FromBase64String(arquivo);

        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagens", imgNome);

        //    if (System.IO.File.Exists(filePath))
        //    {
        //        ModelState.AddModelError(string.Empty, "já existe um arquivo com esse nome!");
        //        return false;
        //    }

        //    System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        //    return true;
        //}
    }


}
