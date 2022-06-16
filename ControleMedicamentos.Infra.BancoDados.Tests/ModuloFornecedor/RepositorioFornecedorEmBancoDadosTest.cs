using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest : BaseTest
    {
        public RepositorioFornecedorEmBancoDadosTest()
        {
        }

        [TestMethod]
        public void Deve_inserir_fornecedor()
        {
            //arrange
            Fornecedor novoFornecedor = new()
            {
                Nome = "Edu",
                Telefone = "09876543212",
                Email = "blablabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorio = new RepositorioFornecedorEmBancoDados();

            //action
            repositorio.Inserir(novoFornecedor);

            //assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(novoFornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(novoFornecedor.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(novoFornecedor.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(novoFornecedor.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(novoFornecedor.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(novoFornecedor.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(novoFornecedor.Estado, fornecedorEncontrado.Estado);
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            //arrange
            Fornecedor novoFornecedor = new()
            {
                Nome = "Edu",
                Telefone = "09876543212",
                Email = "blablabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(novoFornecedor);

            Fornecedor fornecedorAtualizado = repositorio.SelecionarPorNumero(novoFornecedor.Id);
            fornecedorAtualizado.Nome = "Edu";
            fornecedorAtualizado.Telefone = "09876543212";
            fornecedorAtualizado.Email = "blablabla@gmail.com";
            fornecedorAtualizado.Cidade = "Lages";
            fornecedorAtualizado.Estado = "Santa Catarina";

            //action
            repositorio.Editar(fornecedorAtualizado);

            //assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(novoFornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(novoFornecedor.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(novoFornecedor.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(novoFornecedor.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(novoFornecedor.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(novoFornecedor.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(novoFornecedor.Estado, fornecedorEncontrado.Estado);
        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            //arrange
            Fornecedor novoFornecedor = new()
            {
                Nome = "Edu",
                Telefone = "09876543212",
                Email = "blablabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorio = new RepositorioFornecedorEmBancoDados();
            repositorio.Inserir(novoFornecedor);

            //assert
            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(novoFornecedor.Id);

            //action
            repositorio.Excluir(fornecedorEncontrado);

            //assert
            Assert.IsNotNull(fornecedorEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_fornecedor()
        {
            //arrange
            var repositorio = new RepositorioFornecedorEmBancoDados();

            Fornecedor Fornecedor1 = new()
            {
                Nome = "Edu",
                Telefone = "09876543212",
                Email = "blablabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };
            repositorio.Inserir(Fornecedor1);

            Fornecedor Fornecedor2 = new()
            {
                Nome = "Lucas",
                Telefone = "09796553212",
                Email = "blablablabla@gmail.com",
                Cidade = "Florianopolis",
                Estado = "Santa Catarina"
            };
            repositorio.Inserir(Fornecedor2);

            Fornecedor Fornecedor3 = new()
            {
                Nome = "João",
                Telefone = "19873543802",
                Email = "blablabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };
            repositorio.Inserir(Fornecedor3);

            var fornecedores = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, fornecedores.Count);

            Assert.AreEqual("Edu", fornecedores[0].Nome);
            Assert.AreEqual("Lucas", fornecedores[1].Nome);
            Assert.AreEqual("João", fornecedores[2].Nome);
        }
    }
}
