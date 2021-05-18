using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Data;

namespace TCC.Models
{
    public class Requisicao : IRequisicao
    {
        

        public int IdRequisicao { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataRequisicao { get; set; }
        public string Assunto { get; set; }
        public string Descricao { get; set; }
        //public Blog AnexoDocumento { get; set; }
        public string Pathanexodocumento { get; set; }
        public string StatusRequisicao { get; set; }
        public string Situacao { get; set; }
        public string Motivo { get; set; }
        public string Titulacao { get; set; }



        private readonly IRequisicaoRepositorio _requisicaoRepositorio;
        public Requisicao(IRequisicaoRepositorio requisicaoRepositorio)
        {
            _requisicaoRepositorio = requisicaoRepositorio;
        }

        public Requisicao()
        {

        }


        public async Task<IEnumerable<Requisicao>> BuscarTodos()
        {
            var resultado = await _requisicaoRepositorio.BuscarTodos();
            return resultado;
        }

        public async Task<bool> CadastrarAsync(Requisicao requisicao)
        {
            var resultado = await _requisicaoRepositorio.CadastrarAsync(requisicao);

            return resultado;
        }

        public async Task<Requisicao> BuscarPorEmail(string email)
        {
            var resultado = await _requisicaoRepositorio.BuscarPorUsuario(email);
            return resultado;
        }

    }
    
    public interface IRequisicao 
    {
        Task<IEnumerable<Requisicao>> BuscarTodos();
        Task<bool> CadastrarAsync(Requisicao requisicao);
        Task<Requisicao> BuscarPorEmail(string email);
    }

}
