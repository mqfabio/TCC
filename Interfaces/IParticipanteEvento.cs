using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Interfaces
{
    public interface IParticipante_evento
    {
        Task<bool> CadastrarAsync(Participante_evento pe);
        Task<Participante_evento> BuscarPeloUsuarioAsync(int idUsuario, int idEvento);
        Task<bool> DeletarAsync(int idUsuario, int idevento);
    }
}


