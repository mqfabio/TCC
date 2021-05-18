using System.Collections.Generic;
using System.Threading.Tasks;
using TCC.Data;

//namespace TCC.Models
//{
//    public class Excluido : IExcluido
//    {
//        public int Id { get; set; }
//        public string Senha { get; set; }
//        public string Perfil { get; set; }
//        public string Nome { get; set; }

//        private readonly IExcluidoRepositorio _usuarioRepositorio;

//        public Excluido(IExcluidoRepositorio usuarioRepositorio)
//        {
//            _usuarioRepositorio = usuarioRepositorio;
//        }

//        public Excluido()
//        {

//        }


//        public async Task<bool> CadastrarAsync(Excluido usuario)
//        {
//            var resultado = await _usuarioRepositorio.CadastrarAsync(usuario);

//            return resultado;
//        }

//        public async Task<IEnumerable<Excluido>> BuscarTodos()
//        {
//            var resultado = await _usuarioRepositorio.BuscarTodos();

//            return resultado;
//        }
//    }

//    public interface IExcluido
//    {
//        Task<bool> CadastrarAsync(Excluido usuario);
//        Task<IEnumerable<Excluido>> BuscarTodos();
//    }

//}
