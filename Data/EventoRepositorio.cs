using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TCC.DTO;
using TCC.Enums;
using TCC.Interfaces;
using TCC.Models;

namespace TCC.Data
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly string connectStringLocal;
        private readonly string connectStringSomee;
        public EventoRepositorio(IConfiguration config)
        {
            connectStringLocal = config["connectStringLocal"];
            connectStringSomee = config["connectStringSomee"];
        }

        public async Task<bool> CadastrarAsync(Evento evento)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"INSERT INTO [dbo].[evento]
                                ( nome ,descricao ,dataEvento, statusEvento)
                                    Values (@Nome, @Descricao, @DataEvento, @StatusEvento)
                                    select cast (scope_identity() as int)";
                    object o = await conexao.ExecuteScalarAsync(query, evento, commandType: CommandType.Text);
                    if (o != null)
                    {
                        evento.IdEvento = Convert.ToInt32(o);
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                    //var resultado = await conexao.ExecuteAsync(query, evento, commandType: CommandType.Text);
                    //return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> AlterarAsync(Evento evento)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"UPDATE [dbo].[evento] set
                                        nome = @nome ,
                                        descricao = @descricao, 
                                        dataEvento = @dataEvento, 
                                        statusEvento = @statusEvento
                                WHERE idEvento = @idEvento";

                    var resultado = await conexao.ExecuteAsync(query, evento, commandType: CommandType.Text);
                    return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var param = new { id = id };

                    var query = @"DELETE [dbo].[evento] Where IdEvento = @id";

                    var resultado = await conexao.ExecuteAsync(query, param, commandType: CommandType.Text);

                    return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
            
        }

        public async Task<Evento> BuscarPorNomeAsync(string nome)
        {
            try
            {                         
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"select  idEvento, nome, descricao, dataEvento, statusEvento from evento Where Nome = @nome";

                    var param = new { nome = nome };
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Evento>(query, param);

                    return resultado.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Evento>> BuscarPelaDataAsync(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"select  idEvento, nome, descricao, dataEvento, statusEvento from evento Where  format(DataEvento,'yyyy-MM-dd')  
                                                                      between format(@dataInicio,'yyyy-MM-dd') and format(@dataFim,'yyyy-MM-dd')";

                    var param = new { dataInicio = dataInicio, dataFim = dataFim};
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Evento>(query, param);

                    return resultado;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EventoComUsuariosParticipantes>> BuscarTodosAsync()
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"select 
                                        p.idEvento 'IdEvento',
                                        e.nome 'Nome',
                                        e.descricao 'Descricao',
                                        e.dataevento 'DataEvento',
                                        u.nomeusuario 'NomeUsuario',
                                        e.statusEvento 'StatusEvento'
                                from 
                                        evento e 
                                LEFT JOIN 
                                        participante_evento p ON e.idEvento = p.idEvento 
                                LEFT JOIN 
                                        usuario u ON p.idusuario = u.idusuario";

                    var resultado = await conexao.QueryAsync<EventoDoUsuarioDTO>(query);

                    var listaRetorno = new List<EventoComUsuariosParticipantes>();
                    var resultadoAgrupadoPorCurso = resultado.GroupBy(x => x.IdEvento).OrderBy(x => x.Key).ToList();
                    Console.WriteLine("entrou");
                    resultadoAgrupadoPorCurso.ForEach((Action<IGrouping<int, EventoDoUsuarioDTO>>)(evento =>
                    {
                        listaRetorno.Add(new EventoComUsuariosParticipantes
                        {
                            IdEvento = evento.Key,
                            Nome = evento.Select(x => x.Nome).FirstOrDefault(),
                            Descricao = evento.Select(x => x.Descricao).FirstOrDefault(),
                            Participantes = evento.Select(x => x.NomeUsuario).ToList(),
                            DataEvento = evento.Select(x => x.DataEvento).FirstOrDefault(),
                            StatusEvento = evento.Select(x => x.StatusEvento).FirstOrDefault()
                        }
                        );
                    }));

                    return listaRetorno;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<EventoComUsuariosParticipantes>> BuscarEventosPeloNomeouDataTrazendoUsuarioAsync(string nomeEvento, 
                                                                                                                DateTime? dataInicio, 
                                                                                                                DateTime? datafim)
        {
            try
            {
                
                using (var conexao = new SqlConnection(connectStringSomee))
                    {
                    if(nomeEvento != null) nomeEvento = nomeEvento + '%';
                    var param = new { nomeEvento = nomeEvento, dataInicio = dataInicio, datafim = datafim};
                    var query = @"select 
                                        p.idEvento 'IdEvento',
                                        e.nome 'Nome',
                                        e.descricao 'Descricao',
                                        e.dataevento 'DataEvento',
                                        u.nomeusuario 'NomeUsuario',
                                        e.statusEvento 'StatusEvento'
                                from 
                                        evento e 
                                LEFT JOIN 
                                        participante_evento p ON e.idEvento = p.idEvento 
                                LEFT JOIN 
                                        usuario u ON p.idusuario = u.idusuario
                                WHERE e.nome  LIKE @nomeEvento or format(DataEvento,'yyyy-MM-dd')  
                                                                      between format(@dataInicio,'yyyy-MM-dd') and format(@datafim,'yyyy-MM-dd')";
                    var resultado = await conexao.QueryAsync<EventoDoUsuarioDTO>(query, param);

                    var listaRetorno = new List<EventoComUsuariosParticipantes>();
                    var resultadoAgrupadoPorCurso = resultado.GroupBy(x => x.IdEvento).OrderBy(x => x.Key).ToList();
                    resultadoAgrupadoPorCurso.ForEach((Action<IGrouping<int, EventoDoUsuarioDTO>>)(evento =>
                    {
                        listaRetorno.Add(new EventoComUsuariosParticipantes
                        {
                            IdEvento = evento.Key,
                            Nome = evento.Select(x => x.Nome).FirstOrDefault(),
                            Descricao = evento.Select(x => x.Descricao).FirstOrDefault(),
                            Participantes = evento.Select(x => x.NomeUsuario).ToList(),
                            DataEvento = evento.Select(x => x.DataEvento.Date).FirstOrDefault(),
                            StatusEvento = evento.Select(x => x.StatusEvento).FirstOrDefault()
                        }
                        );                       
                    }));


                    return listaRetorno;
                } 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Evento>> BuscarPeloRmAsync(int rm)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var param = new { rm = rm };
                    var query = @"select 
                                        p.idEvento,
                                        e.nome,
                                        e.descricao,
                                        e.dataevento,
                                        u.nomeusuario,
                                        e.statusEvento 
                                from 
                                        evento e
                                LEFT JOIN 
                                        participante_evento p ON e.idEvento = p.idEvento 
                                LEFT JOIN 
                                        usuario u ON p.idusuario = u.idusuario
                                WHERE u.RM = @rm";
                    
                    var resultado = await conexao.QueryAsync<Evento>(query, param);

                    return resultado;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> InativarAsync(Evento evento)
        {
            try
            {
                evento.StatusEvento = StatusEventoEnum.FECHADO;
                await AlterarAsync(evento);
                return evento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
 
    }
}
