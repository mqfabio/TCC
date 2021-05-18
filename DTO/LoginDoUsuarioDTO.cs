using TCC.Enums;

namespace TCC.DTO
{
    public class LoginDoUsuarioDTO
    {
        public string Nome { get; set; }
        public PerfilEnum Perfil { get; set; }
        public string Token { get; set; }

    }
}
