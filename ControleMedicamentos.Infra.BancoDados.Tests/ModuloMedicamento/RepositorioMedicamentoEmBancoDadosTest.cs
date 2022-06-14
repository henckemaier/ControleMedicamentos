using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        private readonly Medicamento medicamento;
        private readonly RepositorioMedicamentoEmBancoDados repositorioMedicamento;
        private readonly RepositorioFornecedorEmBancoDados repositorioFornecedor;

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)

                 DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)";

            Db.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {
        }
    }
}
