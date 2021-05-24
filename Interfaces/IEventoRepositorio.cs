using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Interfaces
{
    public interface IEventoRepositorio
    {
        Task<bool> CadastrarAsync(Evento evento);

        Task<bool> DeletarAsync(int id);

        Task<Evento> BuscarPorNomeAsync(string nome);

        Task<IEnumerable<Evento>> BuscarTodosAsync();

        Task<bool> AlterarAsync(Evento evento);

        Task<List<EventoComUsuariosParticipantes>> BuscarEventosPeloNomeouDataTrazendoUsuarioAsync(string nomeEvento, DateTime? dataInicio, DateTime? datafim);

        Task<IEnumerable<Evento>> BuscarPeloRmAsync(int rm);

        Task<IEnumerable<Evento>> BuscarPelaDataAsync(DateTime dataInicio, DateTime dataFim);

        Task<Evento> InativarAsync(Evento evento);
    }
}
