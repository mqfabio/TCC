using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TCC.Interfaces;
using TCC.Models;

namespace TCC.Data
{
    public class ParticipanteEventoRepositorio : IParticipanteEventoRepositorio
    {

        private readonly string connectStringLocal;
        private readonly string connectStringSomee;
        public ParticipanteEventoRepositorio(IConfiguration config)
        {
            connectStringLocal = config["connectStringLocal"];
            connectStringSomee = config["connectStringSomee"];
        }
        public async Task<bool> CadastrarAsync(Participante_evento pe)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"INSERT INTO [dbo].[participante_evento]
                            (idEvento, idUsuario)
                        values
                            (@IdEvento, @IdUsuario)";
                    var validacao = await BuscarPeloUsuarioAsync(pe.IdUsuario, pe.IdEvento);

                    if(validacao != null) 
                    {
                        return false;
                    }
                    else
                    {
                        var resultado = await conexao.ExecuteAsync(query, pe, commandType: CommandType.Text);
                        return resultado == 1;
                    }
                        

                   
                    
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<Participante_evento> BuscarPeloUsuarioAsync(int idUsuario, int idEvento)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"SELECT idUsuario, idEvento from participante_evento WHERE idUsuario = @idUsuario and idEvento = @idEvento";

                    var param = new { idUsuario = idUsuario, idEvento = idEvento };
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Participante_evento>(query, param);

                    return resultado.FirstOrDefault();
                }

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public async Task<bool> DeletarAsync(int idUsuario, int idEvento)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {

                    var param = new { idUsuario = idUsuario, idEvento = idEvento };
                    var query = @"DELETE [dbo].[Participante_evento] Where idUsuario = @idUsuario and idEvento = @idEvento";

                    var resultado = await conexao.ExecuteAsync(query, param, commandType: CommandType.Text);

                    return resultado == 1;
                }  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

  
}
