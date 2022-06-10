using ControleMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteEmBancoDeDados : IRepositorioPaciente
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=ControleMedimentoDb;" +
               "Integrated Security=True;" +
               "Pooling=False";
        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBPACIENTE] 
                (
                    [NOME],
                    [CARTAOSUS]
	            )
	            VALUES
                (
                    @NOME,
                    @CARTAOSUS
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBPACIENTE]	
		        SET
			        [NOME] = @NOME,
			        [CARTAOSUS] = @CARTAOSUS
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBPACIENTE]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [ID], 
		            [NOME], 
		            [CARTAOSUS]
	            FROM 
		            [TBPACIENTE]";

        private const string sqlSelecionarPorId =
            @"SELECT 
		            [ID], 
		            [NOME], 
		            [CARTAOSUS]
	            FROM 
		            [TBPACIENTE]
		        WHERE
                    [ID] = @ID";
        #endregion

        public ValidationResult Inserir(Paciente novoRegistro)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosPaciente(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public ValidationResult Editar(Paciente registro)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosPaciente(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Paciente registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o paciente :("));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Paciente> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            List<Paciente> pacientes = new List<Paciente>();

            while (leitorPaciente.Read())
            {
                Paciente paciente = ConverterParaPaciente(leitorPaciente);

                pacientes.Add(paciente);
            }

            conexaoComBanco.Close();

            return pacientes;
        }

        public Paciente SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            Paciente paciente = null;
            if (leitorPaciente.Read())
                paciente = ConverterParaPaciente(leitorPaciente);

            conexaoComBanco.Close();

            return paciente;
        }

        private static Paciente ConverterParaPaciente(SqlDataReader leitorPaciente)
        {
            int id = Convert.ToInt32(leitorPaciente["ID"]);
            string nome = Convert.ToString(leitorPaciente["NOME"]);
            string cartaoSUS = Convert.ToString(leitorPaciente["CARTAOSUS"]);

            var paciente = new Paciente
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartaoSUS
            };

            return paciente;
        }

        private void ConfigurarParametrosPaciente(Paciente novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoRegistro.Id);
            comando.Parameters.AddWithValue("NOME", novoRegistro.Nome);
            comando.Parameters.AddWithValue("CARTAOSUS", novoRegistro.CartaoSUS);
        }
    }
}
