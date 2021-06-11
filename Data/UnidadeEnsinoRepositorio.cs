using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TCC.Interfaces;
using TCC.Models;

namespace TCC.Data
{
    public class UnidadeEnsinoRepositorio : IUnidadeEnsinoRepositorio
    {
        private readonly string connectStringLocal;
        private readonly string connectStringSomee;
       
        public UnidadeEnsinoRepositorio(IConfiguration config)
        {
            connectStringLocal = config["connectStringLocal"];
            connectStringSomee = config["connectStringSomee"];
        }

        public async Task<IEnumerable<UnidadeEnsino>> BuscarTodosAsync()
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"select  codUE, nome, endereco, telefone, pathlogotipo from unidadeEnsino";
                    var resultado = await conexao.QueryAsync<UnidadeEnsino>(query);

                    return resultado;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
