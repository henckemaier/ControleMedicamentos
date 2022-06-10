using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest
    {
        public RepositorioPacienteEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)";

            Db.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_paciente()
        {
            //arrange
            Paciente novoPaciente = new()
            {
                Nome = "Edu",
                CartaoSUS = "123456789012345"
            };

            var repositorio = new RepositorioPacienteEmBancoDeDados();

            //action
            repositorio.Inserir(novoPaciente);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(novoPaciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(novoPaciente.Id, pacienteEncontrado.Id);
            Assert.AreEqual(novoPaciente.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(novoPaciente.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_editar_paciente()
        {
            //arrange
            Paciente novoPaciente = new()
            {
                Nome = "Edu",
                CartaoSUS = "123456789012345"
            };

            var repositorio = new RepositorioPacienteEmBancoDeDados();
            repositorio.Inserir(novoPaciente);

            Paciente pacienteAtualizado = repositorio.SelecionarPorNumero(novoPaciente.Id);
            pacienteAtualizado.Nome = "Edu";
            pacienteAtualizado.CartaoSUS = "123456789012345";

            //action
            repositorio.Editar(pacienteAtualizado);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(novoPaciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(novoPaciente.Id, pacienteEncontrado.Id);
            Assert.AreEqual(novoPaciente.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(novoPaciente.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {
            //arrange
            Paciente novoPaciente = new()
            {
                Nome = "Edu",
                CartaoSUS = "123456789012345"
            };

            var repositorio = new RepositorioPacienteEmBancoDeDados();

            repositorio.Inserir(novoPaciente);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(novoPaciente.Id);

            //action
            repositorio.Excluir(pacienteEncontrado);

            //assert
            Assert.IsNotNull(pacienteEncontrado);
        }
    }
}
