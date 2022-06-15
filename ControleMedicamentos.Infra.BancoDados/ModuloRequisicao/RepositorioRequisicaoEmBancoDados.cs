using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
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
                [TBREQUISICAO] AS R LEFT JOIN [TBFUNCIONARIO] AS FU
            ON
                FU.ID = R.FUNCIONARIO_ID
            FROM
                [TBREQUISICAO] AS R LEFT JOIN [TBPACIENTE] AS P
            ON
                P.ID = R.PACIENTE_ID
            FROM
                [TBREQUISICAO] AS R LEFT JOIN [TBMEDICAMENTO] AS M
            ON
                M.ID = R.MEDICAMENTO_ID
            FROM
                [TBREQUISICAO] AS R LEFT JOIN [TBFORNECEDOR] AS FO
            ON
                FO.ID = M.FORNECEDOR_ID";

        private const string sqlSelecionarPorId =
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
                [TBREQUISICAO] AS R LEFT JOIN [TBFUNCIONARIO] AS FU
            ON
                FU.ID = R.FUNCIONARIO_ID
            FROM
                [TBREQUISICAO] AS R LEFT JOIN [TBPACIENTE] AS P
            ON
                P.ID = R.PACIENTE_ID
            FROM
                [TBREQUISICAO] AS R LEFT JOIN [TBMEDICAMENTO] AS M
            ON
                M.ID = R.MEDICAMENTO_ID
            FROM
                [TBREQUISICAO] AS R LEFT JOIN [TBFORNECEDOR] AS FO
            ON
                FO.ID = M.FORNECEDOR_ID
            WHERE 
                R.[ID] = @ID";

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
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao requisicao = ConverterParaRequisicoes(leitorRequisicao);

                requisicoes.Add(requisicao);
            }

            conexaoComBanco.Close();

            return requisicoes;
        }

        private Requisicao ConverterParaRequisicoes(SqlDataReader leitorRequisicao)
        {
            int id = Convert.ToInt32(leitorRequisicao["ID"]);
            int qtdMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            DateTime data = Convert.ToDateTime(leitorRequisicao["DATA"]);

            var requisicao = new Requisicao
            {
                Id = id,
                QtdMedicamento = qtdMedicamento,
                Data = data
            };
            if (leitorRequisicao["FUNCIONARIO_ID"] != DBNull.Value)
            {
                var idFuncionario = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
                var nomeFuncionario = Convert.ToString(leitorRequisicao["NOME"]);
                var login = Convert.ToString(leitorRequisicao["LOGIN"]);
                var senha = Convert.ToString(leitorRequisicao["SENHA"]);

                requisicao.Funcionario = new Funcionario
                {
                    Id = idFuncionario,
                    Nome = nomeFuncionario,
                    Login = login,
                    Senha = senha
                };
            }
            if (leitorRequisicao["PACIENTE_ID"] != DBNull.Value)
            {
                var idPaciente = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
                var nomePaciente = Convert.ToString(leitorRequisicao["NOME"]);
                var cartaoSUS = Convert.ToString(leitorRequisicao["CARTAOSUS"]);

                requisicao.Paciente = new Paciente
                {
                    Id = idPaciente,
                    Nome = nomePaciente,
                    CartaoSUS = cartaoSUS
                };
            }
            if (leitorRequisicao["MEDICAMENTO_ID"] != DBNull.Value)
            {
                var idMedicamento = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
                var nomeMedicamento = Convert.ToString(leitorRequisicao["NOME"]);
                var descricao = Convert.ToString(leitorRequisicao["DESCRICAO"]);
                var lote = Convert.ToString(leitorRequisicao["LOTE"]);
                var validade = Convert.ToDateTime(leitorRequisicao["VALIDADE"]);

                requisicao.Medicamento = new Medicamento
                {
                    Id = idMedicamento,
                    Nome = nomeMedicamento,
                    Descricao = descricao,
                    Lote = lote,
                    Validade = validade,
                };
            }
            return requisicao;
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
