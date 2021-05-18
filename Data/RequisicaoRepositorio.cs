using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TCC.Models;

namespace TCC.Data
{
    public class RequisicaoRepositorio : IRequisicaoRepositorio
    {

        
        public async Task<IEnumerable<Requisicao>> BuscarTodos()
        {
            try
            {
                using (var conexao = new SqlConnection(Configuracao.SomeeConnectString))
                {
                    var query = @"select  idRequisicao, idUsuario, dataRequisicao, assunto, descricao, pathanexodocumento, 
                                statusRequisicao, situacao, motivo, titulacao from requisicao";
                    var resultado = await conexao.QueryAsync<Requisicao>(query);

                    return resultado;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CadastrarAsync(Requisicao requisicao)
        {
            try
            {
                using (var conexao = new SqlConnection(Configuracao.SomeeConnectString))
                {
                    var query = @"INSERT INTO [dbo].[requisicao]
                                (idUsuario, dataRequisicao, assunto, descricao, pathanexodocumento, 
                                statusRequisicao, situacao, motivo, titulacao)
                            Values
                                (@IdUsuario, @DataRequisicao, @Assunto, @Descricao, @Pathanexodocumento, 
                                @StatusRequisicao, @Situacao, @motivo, @Titulacao)";

                    var resultado = await conexao.ExecuteAsync(query, requisicao, commandType: CommandType.Text);

                    return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<Requisicao> BuscarPorUsuario(string email)
        {
            Usuario usuario = new Usuario();
            var user = usuario.PegarPeloEmailAsync(email);
            var idUsuario = user.Id;
            try
            {                           
                using (var conexao = new SqlConnection(Configuracao.SomeeConnectString))
                {
                   
                    var query = @"select  idRequisicao, idUsuario, dataRequisicao, assunto, descricao, pathanexodocumento, 
                                statusRequisicao, situacao, motivo, titulacao where IdUsuario = @usuario.idUsuario";

                    var param = new { email = email };
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Requisicao>(query, param);

                    return resultado.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public interface IRequisicaoRepositorio
    {
        Task<IEnumerable<Requisicao>> BuscarTodos();
        Task<bool> CadastrarAsync(Requisicao requisicao);
        Task<Requisicao> BuscarPorUsuario(string email);
    }
}
