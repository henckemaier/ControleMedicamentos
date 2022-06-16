using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest : BaseTest
    {
        public RepositorioPacienteEmBancoDadosTest()
        {
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

        [TestMethod]
        public void Deve_selecionar_todos_pacientes()
        {
            //arrange
            var repositorio = new RepositorioPacienteEmBancoDeDados();

            Paciente paciente1 = new()
            {
                Nome = "Edu",
                CartaoSUS = "123456789012345"
            };
            repositorio.Inserir(paciente1);

            Paciente paciente2 = new()
            {
                Nome = "Emanuel",
                CartaoSUS = "123558789012645"
            };
            repositorio.Inserir(paciente2);

            Paciente paciente3 = new()
            {
                Nome = "Lucas",
                CartaoSUS = "223456789612348"
            };
            repositorio.Inserir(paciente3);

            //action
            var pacientes = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, pacientes.Count);

            Assert.AreEqual("Edu", pacientes[0].Nome);
            Assert.AreEqual("Emanuel", pacientes[1].Nome);
            Assert.AreEqual("Lucas", pacientes[2].Nome);
        }
    }
}
