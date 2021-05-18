using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCC.Data;
using TCC.Enums;
using TCC.Interfaces;

namespace TCC.Models
{
    public class Evento : IEvento
    {
        public int IdEvento { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
        public StatusEventoEnum StatusEvento { get; set; }
        public List<int> Participantes { get; set; }






        private readonly IEventoRepositorio _eventoRepositorio;

        public Evento(IEventoRepositorio eventoRepositorio)
        {
            _eventoRepositorio = eventoRepositorio;
        }


        public Evento()
        {

        }
        public Evento(string nome, string descricao, DateTime data_evento, string nomeServidor, StatusEventoEnum statusEvento)
        {
            
            Nome = nome;
            Descricao = descricao;
            DataEvento = data_evento;
            StatusEvento = statusEvento;
        }

        public async Task<bool> CadastrarAsync(Evento evento)
        {
            var resultado = await _eventoRepositorio.CadastrarAsync(evento);

            return resultado;
        }

        public async Task<bool> AlterarAsync(Evento evento)
        {
            var resultado = await _eventoRepositorio.AlterarAsync(evento);
            return resultado;
        }

        public async Task<bool> ExcluirAsync(int idEvento)
        {
            var resultado = await _eventoRepositorio.DeletarAsync(idEvento);
            return resultado;
        }

        public async Task<Evento> PegarPeloNomeAsync(string nome)
        {
            var resultado = await _eventoRepositorio.BuscarPorNomeAsync(nome);
            return resultado;
        }

        public async Task<IEnumerable<Evento>> BuscarTodosAsync()
        {
            var resultado = await _eventoRepositorio.BuscarTodosAsync();
            return resultado;
        }

        public async Task<List<EventoComUsuariosParticipantes>> BuscarEventosPeloNomeouDataTrazendoUsuarioAsync(string nomeEvento, DateTime? dataInicio, DateTime? datafim)
        {
            var resultado = await _eventoRepositorio.BuscarEventosPeloNomeouDataTrazendoUsuarioAsync(nomeEvento, dataInicio, datafim);
            return resultado;
        }

        public async Task<IEnumerable<Evento>> BuscarPeloRmAsync(int rm)
        {
            var resultado = await _eventoRepositorio.BuscarPeloRmAsync(rm);
            return resultado;
        }

        public async Task<IEnumerable<Evento>> BuscarPorNomeOuData(DateTime dataInicio, DateTime dataFim)
        {
            var resultado = await _eventoRepositorio.BuscarPelaDataAsync(dataInicio, dataFim);
            return resultado;
        }
    }
}
