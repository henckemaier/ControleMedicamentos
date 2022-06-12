using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        public RepositorioFuncionarioEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            Db.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_funcionario()
        {
            //arrange
            Funcionario novoFuncionario = new()
            {
                Nome = "Edu",
                Login = "EduHB8585",
                Senha = "123456"
            };

            var repositorio = new RepositorioFuncionarioEmBancoDados();

            //action
            repositorio.Inserir(novoFuncionario);

            //assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(novoFuncionario.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(novoFuncionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(novoFuncionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(novoFuncionario.Senha, funcionarioEncontrado.Senha);
        }

        [TestMethod]
        public void Deve_editar_funcionario()
        {
            //arrange
            Funcionario novoFuncionario = new()
            {
                Nome = "Edu",
                Login = "EduHB8585",
                Senha = "123456"
            };

            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(novoFuncionario);

            Funcionario funcionarioAtualizado = repositorio.SelecionarPorNumero(novoFuncionario.Id);
            funcionarioAtualizado.Nome = "Edu";
            funcionarioAtualizado.Login = "EduHB8585";
            funcionarioAtualizado.Senha = "123456";

            //action
            repositorio.Editar(funcionarioAtualizado);

            //assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(novoFuncionario.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(novoFuncionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(novoFuncionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(novoFuncionario.Senha, funcionarioEncontrado.Senha);
        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            //arrange
            Funcionario novoFuncionario = new()
            {
                Nome = "Edu",
                Login = "EduHB8585",
                Senha = "123456"
            };

            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(novoFuncionario);

            //assert
            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(novoFuncionario.Id);

            //action
            repositorio.Excluir(funcionarioEncontrado);

            //assert
            Assert.IsNotNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_funcionarios()
        {
            //arrange
            var repositorio = new RepositorioFuncionarioEmBancoDados();

            Funcionario Funcionario1 = new()
            {
                Nome = "Edu",
                Login = "EduHB8585",
                Senha = "123456"
            };
            repositorio.Inserir(Funcionario1);

            Funcionario Funcionario2 = new()
            {
                Nome = "Lucas",
                Login = "LucasXRM4854",
                Senha = "5674688"
            };
            repositorio.Inserir(Funcionario2);

            Funcionario Funcionario3 = new()
            {
                Nome = "Fabio",
                Login = "FABIOlb23",
                Senha = "5686545345"
            };
            repositorio.Inserir(Funcionario3);

            var funcionarios = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual("Edu", funcionarios[0].Nome);
            Assert.AreEqual("Lucas", funcionarios[1].Nome);
            Assert.AreEqual("Fabio", funcionarios[2].Nome);
        }
    }
}
