using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDadosTest
    {
        public RepositorioRequisicaoEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)

                DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)

                DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)

                DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)

                 DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)";

            Db.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_requisicao()
        {
            Funcionario novoFuncionario = new()
            {
                Nome = "Edu",
                Login = "edu2345",
                Senha = "1234567"
            };

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();

            repositorioFuncionario.Inserir(novoFuncionario);

            Medicamento novoMedicamento = new()
            {
                Nome = "Decongeste",
                Descricao = "Bom para febre",
                Lote = "123",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 12,
                Fornecedor = novoFuncionario
            };

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            //action
            repositorio.Inserir(novoMedicamento);

            //assert
            Medicamento medicamentoEncontrado = repositorio.SelecionarPorNumero(novoMedicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(novoMedicamento.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(novoMedicamento.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(novoMedicamento.Descricao, medicamentoEncontrado.Descricao);
            Assert.AreEqual(novoMedicamento.Lote, medicamentoEncontrado.Lote);
            Assert.AreEqual(novoMedicamento.Validade, medicamentoEncontrado.Validade);
            Assert.AreEqual(novoMedicamento.QuantidadeDisponivel, medicamentoEncontrado.QuantidadeDisponivel);
            Assert.AreEqual(novoMedicamento.Fornecedor.Id, medicamentoEncontrado.Fornecedor.Id);
        }
    }
    }
}
