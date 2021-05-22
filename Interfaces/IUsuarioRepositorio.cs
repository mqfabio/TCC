using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Interfaces
{
    public interface IUsuarioRepositorio
    {
        //Task<bool> AlterarAsync(Servidor servidor);
        Task<bool> CadastrarAsync(Usuario servidor);
        Task<IEnumerable<Usuario>> BuscarTodosAsync();
        Task<Usuario> BuscarPorEmailESenhaAsync(string nome, string senha);
        Task<bool> AlterarAsync(Usuario usuario);
        Task<Usuario> BuscarPorEmailAsync(string email);
        Task<Usuario> BuscarPorRMAsync(int rm);
        Task<Usuario> BuscarPeloNomeAsync(string nome);
        Task<IEnumerable<Usuario>> BuscarPorRMOuNomeAsync(int rm, string nomeUsuario);
        Task<Usuario> InativarAsync(Usuario usuario);
    }
}
