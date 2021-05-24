using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TCC.Enums;
using TCC.Interfaces;
using TCC.Models;

namespace TCC.Data
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string connectStringLocal;
        private readonly string connectStringSomee;
        public UsuarioRepositorio(IConfiguration config)
        {
            connectStringLocal = config["connectStringLocal"];
            connectStringSomee = config["connectStringSomee"];
        }
        public async Task<Usuario> BuscarPorEmailAsync(string email)
        {
           
            try
            {
                using (var conexao = new SqlConnection(connectStringLocal))
                {
                    var query = @"select  
                                        idUsuario, 
                                        senha, 
                                        codUE, 
                                        RM, 
                                        CPF, 
                                        RG, 
                                        dataNascimento, 
                                        nomeUsuario, 
                                        email, 
                                        statusUsuario, 
                                        perfil, 
                                        titulacao, 
                                        cargo 
                                from 
                                    usuario 
                                Where email = @email and statusUsuario = 0 ";

                    var param = new { email = email};
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Usuario>(query, param);

                    return resultado.FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario> BuscarPorEmailESenhaAsync(string email, string senha)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringLocal))
                {
                    var resultado2 = await BuscarPorEmailAsync(email);

                    bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, resultado2.Senha);

                    if (senhaValida)
                    {
                        return resultado2;  
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CadastrarAsync(Usuario usuario)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"INSERT INTO [dbo].[usuario]
                                ( senha, codUE, RM, CPF, RG, dataNascimento, nomeUsuario, email, statusUsuario, perfil, titulacao, cargo)
                            Values
                                (@Senha, @CodUE, @RM, @CPF, @RG, @DataNascimento, @NomeUsuario, @email,@StatusUsuario, @Perfil, @Titulacao, @cargo)";

                    var validacao = await BuscarPorEmailAsync(usuario.Email);

                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

                    if (validacao == null)
                    {
                        var resultado = await conexao.ExecuteAsync(query, usuario, commandType: CommandType.Text);
                        return resultado == 1;
                    }

                    return false;

                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<Usuario>> BuscarTodosAsync()
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringLocal))
                {
                    var query = @"select  IdUsuario, Senha, CodUE, RM, CPF, RG, DataNascimento, NomeUsuario, Email, Cargo, 
                                          StatusUsuario, Perfil from usuario";
                    var resultado = await conexao.QueryAsync<Usuario>(query);

                    return resultado;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<bool> AlterarAsync(Usuario usuario)
        {
            try
            {
                using (var conexao = new SqlConnection(connectStringLocal))
                {
                    var query = @"UPDATE [dbo].[usuario] set
                                senha = @senha, 
                                codUE = @codUE, 
                                RM = @RM, 
                                CPF = CPF, 
                                RG = RG, 
                                dataNascimento = @dataNascimento, 
                                nomeUsuario = @nomeUsuario, 
                                email = @email, 
                                cargo = @cargo, 
                                statusUsuario = @statusUsuario, 
                                perfil = @perfil
                            WHERE idUSuario = @idUsuario";

                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

                    var resultado = await conexao.ExecuteAsync(query, usuario, commandType: CommandType.Text);
                    

                    return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario> BuscarPorRMAsync(int rm)
        {

            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"select  
                                        idUsuario, 
                                        senha, 
                                        codUE, 
                                        RM, 
                                        CPF, 
                                        RG, 
                                        dataNascimento, 
                                        nomeUsuario, 
                                        email, 
                                        statusUsuario, 
                                        perfil, 
                                        titulacao, 
                                        cargo 
                                from 
                                    usuario 
                                Where RM = @rm and statusUsuario = 0 ";

                    var param = new { RM = rm };
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Usuario>(query, param);

                    return resultado.FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario> BuscarPeloNomeAsync(string nome)
        {

            try
            {
                using (var conexao = new SqlConnection(connectStringSomee))
                {
                    var query = @"select  
                                        idUsuario, 
                                        senha, 
                                        codUE, 
                                        RM, 
                                        CPF, 
                                        RG, 
                                        dataNascimento, 
                                        nomeUsuario, 
                                        email, 
                                        statusUsuario, 
                                        perfil, 
                                        titulacao, 
                                        cargo 
                                from 
                                    usuario 
                                Where nomeUsuario = @nome and statusUsuario = 0 ";

                    var param = new { nome = nome };
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Usuario>(query, param);

                    return resultado.FirstOrDefault();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Usuario>> BuscarPorRMOuNomeAsync(int rm, string nomeUsuario)
        {

            try
            {
                using (var conexao = new SqlConnection(connectStringLocal))
                {
                    var query = "";
                    nomeUsuario = nomeUsuario + "%";

                    if (rm != 0 && nomeUsuario != null)
                    {
                         query = @"select  
                                        idUsuario, 
                                        senha, 
                                        codUE, 
                                        RM, 
                                        CPF, 
                                        RG, 
                                        dataNascimento, 
                                        nomeUsuario, 
                                        email, 
                                        statusUsuario, 
                                        perfil, 
                                        titulacao, 
                                        cargo 
                                from 
                                    usuario 
                                Where RM = @rm and nomeUsuario LIKE @nomeUsuario and statusUsuario = 0 ";

                    }
                    else
                    query = @"select  
                                        idUsuario, 
                                        senha, 
                                        codUE, 
                                        RM, 
                                        CPF, 
                                        RG, 
                                        dataNascimento, 
                                        nomeUsuario, 
                                        email, 
                                        statusUsuario, 
                                        perfil, 
                                        titulacao, 
                                        cargo 
                                from 
                                    usuario 
                                Where RM = @rm or nomeUsuario LIKE @nomeUsuario and statusUsuario = 0 ";

                   

                    var param = new { RM = rm, nomeUsuario = nomeUsuario };
                    conexao.Open();
                    var resultado = await conexao.QueryAsync<Usuario>(query, param);

                    return resultado;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario> InativarAsync(Usuario usuario)
        {
            try
            {
                usuario.StatusUsuario = StatusUsuarioEnum.inativo;
                await AlterarAsync(usuario);
                return usuario;           
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}


