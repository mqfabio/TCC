using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Interfaces;

namespace TCC.Models
{
    public class UnidadeEnsino : IUnidadeEnsino
    {
        public int CodUE { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Pathlogotipo { get; set; }


        private readonly IUnidadeEnsinoRepositorio _unidadeEnsinoRepositorio;


        public UnidadeEnsino(IUnidadeEnsinoRepositorio unidadeEnsinoRepositorio)
        {
            _unidadeEnsinoRepositorio = unidadeEnsinoRepositorio;
        }


        public UnidadeEnsino()
        {

        }

        public async Task<IEnumerable<UnidadeEnsino>> BuscarTodosAsync()
        {
            var resultado = await _unidadeEnsinoRepositorio.BuscarTodosAsync();
            return resultado;
        }


    }
}
