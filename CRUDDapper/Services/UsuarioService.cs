using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using AutoMapper;
using Azure;
using CRUDDapper.DTO;
using CRUDDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace CRUDDapper.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        //injeção de dependência
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsuarioService(IConfiguration configuration, IMapper mapper) 
        {
           _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<UsuarioListarDTO>> BuscarUsuarioPorId(int usuarioId)
        {
            ResponseModel<UsuarioListarDTO> response = new ResponseModel<UsuarioListarDTO>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var usuariosBanco = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from Usuarios where id = @Id", new{Id = usuarioId});

                if (usuariosBanco == null)
                {
                    response.Mensagem = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;

                }

                var usuarioMapeado = _mapper.Map<UsuarioListarDTO>(usuariosBanco);
                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuário localizado com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> BuscarUsuarios()
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();


            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryAsync<Usuario>("select * from Usuarios");
                
                if (usuariosBanco.Count() == 0) 
                {
                    response.Mensagem = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }
                //Transformação Mapper
                var usuarioMapeado = _mapper.Map<List<UsuarioListarDTO>>(usuariosBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuários localizados com sucesso!";

            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync("insert into Usuarios(NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) values (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", usuarioCriarDTO);

                if(usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);
                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso!";

            }

            return response;
        }
        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("select * from Usuarios");
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var usuariosBanco = await connection.ExecuteAsync("update Usuarios set NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, Salario = @Salario, Situacao = @Situacao, CPF = @CPF where Id = @Id", usuarioEditarDTO);

                if(usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edição";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);
                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);
                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso!";

            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioListarDTO>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioListarDTO>> response = new ResponseModel<List<UsuarioListarDTO>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.ExecuteAsync("delete from Usuarios where Id = @Id", new {Id = usuarioId});

                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edição";
                    response.Status = false;
                    return response;
                }

                var usuarios= await ListarUsuarios(connection);
                var usuariosMapeados = _mapper.Map<List<UsuarioListarDTO>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso!";


            }

            return response;
        }
    }
}
