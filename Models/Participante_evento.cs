using System.Threading.Tasks;
using TCC.Interfaces;

namespace TCC.Models
{
    public class Participante_evento : IParticipante_evento
    {


        public int IdEvento { get; set; }
        public int IdUsuario { get; set; }



        private readonly IParticipanteEventoRepositorio _participanteEventoRepositorio;
        public Participante_evento(IParticipanteEventoRepositorio participanteEventoRepositorio)
        {
            _participanteEventoRepositorio = participanteEventoRepositorio;
        }

        public Participante_evento()
        {

        }


        public async Task<bool> CadastrarAsync(Participante_evento pe)
        {
            return await _participanteEventoRepositorio.CadastrarAsync(pe);
        }

        public async Task<Participante_evento> BuscarPeloUsuarioAsync(int idUsuario, int idEvento)
        {
            return await _participanteEventoRepositorio.BuscarPeloUsuarioAsync(idUsuario, idEvento);
        }

        public async Task<bool> DeletarAsync(int idUsuario, int idevento)
        {
            return await _participanteEventoRepositorio.DeletarAsync(idUsuario, idevento);
        }


    }
}

