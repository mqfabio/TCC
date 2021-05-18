using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.DTO
{
    public class EventoDoUsuarioDTO
    {
        public int IdEvento { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
        public string StatusEvento { get; set; }
        public string NomeUsuario { get; set; }
        
    }
}
