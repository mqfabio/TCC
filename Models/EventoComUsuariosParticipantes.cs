using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Enums;

namespace TCC.Models
{
    public class EventoComUsuariosParticipantes
    {
        public int IdEvento { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
        public string StatusEvento { get; set; }
        public List<string> Participantes { get; set; }
    }
}
