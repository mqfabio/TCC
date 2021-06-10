using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TCC.Interfaces;

namespace TCC.Controllers
{
    [Authorize]
    [ApiController]
    [Route("unidadeEnsino")]
    public class UnidadeEnsinoController : ControllerBase
    {
        private readonly IUnidadeEnsino _unidadeEnsino;

        public UnidadeEnsinoController(IUnidadeEnsino unidadeEnsino)
        {
            _unidadeEnsino = unidadeEnsino;
        }
       
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> BuscarTodosAsync()
        {
            var resultado = await _unidadeEnsino.BuscarTodosAsync();
            try
            {
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                    return NotFound();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
