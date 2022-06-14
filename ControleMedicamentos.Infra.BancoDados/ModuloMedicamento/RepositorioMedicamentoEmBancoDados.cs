using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : IRepositorioMedicamento
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=ControleMedimentoDb;" +
               "Integrated Security=True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBMEDICAMENTO] 
                (
                    [NOME],
                    [DESCRICAO],
                    [LOTE],
                    [VALIDADE],
                    [QUANTIDADEDISPONIVEL],
                    [FORNECEDOR_ID]
	            )
	            VALUES
                (
                    @NOME,
                    @DESCRICAO,
                    @LOTE,
                    @VALIDADE,
                    @QUANTIDADEDISPONIVEL,
                    @FORNECEDOR_ID
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBMEDICAMENTO]	
		            SET
		    	        [NOME] = @NOME,
		    	        [DESCRICAO] = @DESCRICAO,
                        [LOTE] = @LOTE,
                        [VALIDADE] = @VALIDADE,
                        [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL,
                        [FORNECEDOR_ID] = @FORNECEDOR_ID
		            WHERE
		    	        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBMEDICAMENTO]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
                CP.[ID],       
                CP.[NOME],
                CP.[DESCRICAO],
                CP.[LOTE],             
                CP.[VALIDADE],                    
                CP.[QUANTIDADEDISPONIVEL],                                
                CP.[FORNECEDOR_ID],
                CT.[NOME],       
                CT.[TELEFONE],             
                CT.[EMAIL],                    
                CT.[CIDADE], 
                CT.[ESTADO] 
            FROM
                [TBMEDICAMENTO] AS CP LEFT JOIN 
                [TBFORNECEDOR] AS CT
            ON
                CT.ID = CP.FORNECEDOR_ID";

        private const string sqlSelecionarPorId =
            @"SELECT 
                CP.[ID],       
                CP.[NOME],
                CP.[DESCRICAO],
                CP.[LOTE],             
                CP.[VALIDADE],                    
                CP.[QUANTIDADEDISPONIVEL],                                
                CP.[FORNECEDOR_ID],
                CT.[NOME],       
                CT.[TELEFONE],             
                CT.[EMAIL],                    
                CT.[CIDADE], 
                CT.[ESTADO] 
            FROM
                [TBMEDICAMENTO] AS CP LEFT JOIN 
                [TBFORNECEDOR] AS CT
            ON
                CT.ID = CP.FORNECEDOR_ID
            WHERE 
                CP.[ID] = @ID";
        #endregion
        public ValidationResult Inserir(Medicamento novoRegistro)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMedicamento(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public ValidationResult Editar(Medicamento registro)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMedicamento(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o medicamento :("));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }
        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> medicamentos = new List<Medicamento>();

            while (leitorMedicamento.Read())
            {
                Medicamento medicamento = ConverterParaMedicamento(leitorMedicamento);

                medicamentos.Add(medicamento);
            }

            conexaoComBanco.Close();

            return medicamentos;
        }

        public Medicamento SelecionarPorNumero(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamento = null;
            if (leitorMedicamento.Read())
                medicamento = ConverterParaMedicamento(leitorMedicamento);

            conexaoComBanco.Close();

            return medicamento;
        }

        private Medicamento ConverterParaMedicamento(SqlDataReader leitorMedicamento)
        {
            int id = Convert.ToInt32(leitorMedicamento["ID"]);
            string nome = Convert.ToString(leitorMedicamento["NOME"]);
            string descricao = Convert.ToString(leitorMedicamento["DESCRICAO"]);
            string lote = Convert.ToString(leitorMedicamento["LOTE"]);
            DateTime validade = Convert.ToDateTime(leitorMedicamento["VALIDADE"]);
            int quantidadeDisponivel = Convert.ToInt32(leitorMedicamento["QUANTIDADEDISPONIVEL"]);

            var medicamento = new Medicamento
            {
                Id = id,
                Nome = nome,
                Descricao = descricao,
                Lote = lote,
                Validade = validade,
                QuantidadeDisponivel = quantidadeDisponivel
            };
            if (leitorMedicamento["FORNECEDOR_ID"] != DBNull.Value)
            {
                var idFornecedor = Convert.ToInt32(leitorMedicamento["FORNECEDOR_ID"]);
                var nomeFornecedor = Convert.ToString(leitorMedicamento["NOME"]);
                var telefone = Convert.ToString(leitorMedicamento["TELEFONE"]);
                var email = Convert.ToString(leitorMedicamento["EMAIL"]);
                var cidade = Convert.ToString(leitorMedicamento["CIDADE"]);
                var estado = Convert.ToString(leitorMedicamento["ESTADO"]);

                medicamento.Fornecedor = new Fornecedor
                {
                    Id = idFornecedor,
                    Nome = nomeFornecedor,
                    Telefone = telefone,
                    Email = email,
                    Cidade = cidade,
                    Estado = estado
                };
            }
            return medicamento;
        }

        private void ConfigurarParametrosMedicamento(Medicamento novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoRegistro.Id);
            comando.Parameters.AddWithValue("NOME", novoRegistro.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", novoRegistro.Descricao);
            comando.Parameters.AddWithValue("LOTE", novoRegistro.Lote);
            comando.Parameters.AddWithValue("VALIDADE", novoRegistro.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", novoRegistro.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", novoRegistro.Fornecedor == null ? DBNull.Value : novoRegistro.Fornecedor.Id);
        }
    }
}
