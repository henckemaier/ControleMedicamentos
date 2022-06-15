using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados : IRepositorioRequisicao
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=ControleMedimentoDb;" +
               "Integrated Security=True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBREQUISICAO] 
                (
                    [FUNCIONARIO_ID],
                    [PACIENTE_ID],
                    [MEDICAMENTO_ID],
                    [QUANTIDADEMEDICAMENTO],
                    [DATA]
	            )
	            VALUES
                (
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,
                    @MEDICAMENTO_ID,
                    @QUANTIDADEMEDICAMENTO,
                    @DATA
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBREQUISICAO]	
		            SET
		    	        [FUNCIONARIO_ID] = @FUNCIONARIO_ID,
		    	        [PACIENTE_ID] = @PACIENTE_ID,
                        [MEDICAMENTO_ID] = @MEDICAMENTO_ID,
                        [QUANTIDADEMEDICAMENTO] = @QUANTIDADEMEDICAMENTO,
                        [DATA] = @DATA
		            WHERE
		    	        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBREQUISICAO]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
                R.[ID],       
                R.[FUNCIONARIO_ID],
                R.[PACIENTE_ID],
                R.[MEDICAMENTO_ID],             
                R.[QUANTIDADEMEDICAMENTO],                    
                R.[DATA],        

                FU.[NOME],       
                FU.[LOGIN],             
                FU.[SENHA],

                P.[NOME],
                P.[CARTAOSUS],

                M.[NOME],
                M.[DESCRICAO],
                M.[LOTE],
                M.[VALIDADE],
                M.[QUANTIDADEDISPONIVEL],
                M.[FORNECEDOR_ID],

                FO.[NOME],
                FO.[TELEFONE],
                FO.[EMAIL],
                FO.[CIDADE],
                FO.[ESTADO]
            FROM
                [TBREQUISICAO] R LEFT JOIN [TBFUNCIONARIO] AS FU
            ON
                FU.ID = R.FUNCIONARIO_ID LEFT JOIN TBPACIENTE P
            ON 
                P.ID = R.MEDICAMENTO_ID";
        #endregion

        public ValidationResult Inserir(Requisicao novoRegistro)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Requisicao registro)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Requisicao registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover a requisição :("));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Requisicao> SelecionarTodos()
        {
            throw new NotImplementedException();
        }

        public Requisicao SelecionarPorNumero(int numero)
        {
            throw new NotImplementedException();
        }

        private void ConfigurarParametrosRequisicao(Requisicao novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoRegistro.Id);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", novoRegistro.Funcionario == null ? DBNull.Value : novoRegistro.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", novoRegistro.Paciente == null ? DBNull.Value : novoRegistro.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", novoRegistro.Medicamento == null ? DBNull.Value : novoRegistro.Medicamento.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", novoRegistro.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", novoRegistro.Data);
        }
    }
}
