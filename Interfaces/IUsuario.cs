using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Interfaces
{
    public interface IUsuario
    {
        Task<bool> CadastrarAsync(Usuario servidor);
        //Task<bool> AlterarAsync(Servidor servidor);
        Task<bool> Inativar(int idServidor);
        Task<IEnumerable<Usuario>> BuscarTodosAsync();
        Task<Usuario> PegarPeloEmailSenhaAsync(string email, string senha);
        Task<bool> AlterarAsync(Usuario idUsuario);
        Task<Usuario> PegarPeloEmailAsync(string email);
        Task<Usuario> PegarPeloRMAsync(int rm);
        Task<Usuario> PegarPeloNomeAsync(string nome);
        Task<IEnumerable<Usuario>> PegarPeloRMOuNomeAsync(int rm, string nomeUsuario);
    }
}
