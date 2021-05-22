using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCC.Data;
using TCC.Enums;
using TCC.Interfaces;

namespace TCC.Models
{
    public class Usuario : IUsuario
    {
        

        public int IdUsuario { get; set; }
        public int CodUE { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public StatusUsuarioEnum StatusUsuario { get; set; }
        public string Titulacao { get; set; }
        public string Senha { get; set; }
        public int RM { get; set; }
        public PerfilEnum Perfil { get; set; }


        public Usuario()
        {
        }


        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public Usuario(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        //public async Task<bool> AlterarAsync(Servidor servidor)
        //{
        //    var resultado = await _servidorRepositorio.AlterarAsync(servidor);
        //    return resultado;
        //}

        public async Task<bool> CadastrarAsync(Usuario usuario)
        {
            var resultado = await _usuarioRepositorio.CadastrarAsync(usuario);
            return resultado;
        }


        public async Task<IEnumerable<Usuario>> BuscarTodosAsync()
        {
            var resultado = await _usuarioRepositorio.BuscarTodosAsync();
            return resultado;
        }

        public async Task<Usuario> PegarPeloEmailSenhaAsync(string email, string senha)
        {
            var resultado = await _usuarioRepositorio.BuscarPorEmailESenhaAsync(email, senha);
            return resultado;
        }

        public async Task<Usuario> PegarPeloEmailAsync(string email)
        {
            var resultado = await _usuarioRepositorio.BuscarPorEmailAsync(email);
            return resultado;
        }


        public async Task<bool> AlterarAsync(Usuario usuario)
        {
            var resultado = await _usuarioRepositorio.AlterarAsync(usuario);
            return resultado;
        }

        public async Task<Usuario> PegarPeloRMAsync(int rm)
        {
            var resultado = await _usuarioRepositorio.BuscarPorRMAsync(rm);
            return resultado;
        }

        public async Task<Usuario> PegarPeloNomeAsync(string nome)
        {
            var resultado = await _usuarioRepositorio.BuscarPeloNomeAsync(nome);
            return resultado;
        }

        public async Task<IEnumerable<Usuario>> PegarPeloRMOuNomeAsync(int rm, string nomeUsuario)
        {
            var resultado = await _usuarioRepositorio.BuscarPorRMOuNomeAsync (rm, nomeUsuario);
            return resultado;
        }

        public async Task<Usuario> InativarAsync(Usuario usuario)
        {
            var resultado = await _usuarioRepositorio.InativarAsync(usuario);
            return resultado;
        }
    }
}