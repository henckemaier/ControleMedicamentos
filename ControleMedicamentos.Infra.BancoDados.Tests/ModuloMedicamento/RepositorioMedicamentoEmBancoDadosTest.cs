using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            Fornecedor novoFornecedor = new()
            {
                Nome = "Remedios Inc",
                Telefone = "123456789098",
                Email = "blablabla@outlook.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorio2 = new RepositorioFornecedorEmBancoDados();

            repositorio2.Inserir(novoFornecedor);

            Medicamento novoMedicamento = new()
            {
                Nome = "Decongeste",
                Descricao = "Bom para febre",
                Lote = "123",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 12,
                Fornecedor = novoFornecedor
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

        [TestMethod]
        public void Deve_editar_medicamento()
        {
            //arrange
            Fornecedor novoFornecedor = new()
            {
                Nome = "Remedios Inc",
                Telefone = "123456789098",
                Email = "blablabla@outlook.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorio2 = new RepositorioFornecedorEmBancoDados();

            repositorio2.Inserir(novoFornecedor);

            Medicamento novoMedicamento = new()
            {
                Nome = "Decongeste",
                Descricao = "Bom para febre",
                Lote = "123",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 12,
                Fornecedor = novoFornecedor
            };

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorio.Inserir(novoMedicamento);

            Medicamento medicamentoAtualizado = repositorio.SelecionarPorNumero(novoMedicamento.Id);
            medicamentoAtualizado.Nome = "Decongeste";
            medicamentoAtualizado.Descricao = "Bom para febre";
            medicamentoAtualizado.Validade = DateTime.Now.AddDays(30).Date;
            medicamentoAtualizado.QuantidadeDisponivel = 12;
            medicamentoAtualizado.Fornecedor = novoFornecedor;

            //action
            repositorio.Editar(medicamentoAtualizado);

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

        [TestMethod]
        public void Deve_excluir_medicamento()
        {
            //arrange
            Fornecedor novoFornecedor = new()
            {
                Nome = "Remedios Inc",
                Telefone = "123456789098",
                Email = "blablabla@outlook.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorio2 = new RepositorioFornecedorEmBancoDados();

            repositorio2.Inserir(novoFornecedor);

            Medicamento novoMedicamento = new()
            {
                Nome = "Decongeste",
                Descricao = "Bom para febre",
                Lote = "123",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 12,
                Fornecedor = novoFornecedor
            };

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            repositorio.Inserir(novoMedicamento);

            //assert
            Medicamento medicamentoEncontrado = repositorio.SelecionarPorNumero(novoMedicamento.Id);

            //action
            repositorio.Excluir(medicamentoEncontrado);

            //assert
            Assert.IsNotNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_medicamentos()
        {
            var repositorio2 = new RepositorioFornecedorEmBancoDados();
            //arrange
            Fornecedor novoFornecedor1 = new()
            {
                Nome = "Remedios Inc",
                Telefone = "123456789098",
                Email = "blablabla@outlook.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };
            repositorio2.Inserir(novoFornecedor1);

            Fornecedor novoFornecedor2 = new()
            {
                Nome = "Drogaria Inc",
                Telefone = "923356789698",
                Email = "blablablablabla@outlook.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };
            repositorio2.Inserir(novoFornecedor2);

            Fornecedor novoFornecedor3 = new()
            {
                Nome = "Remedios e Curativos do Seu Jorge",
                Telefone = "022356669698",
                Email = "blablablablablablabla@outlook.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };
            repositorio2.Inserir(novoFornecedor3);

            var repositorio = new RepositorioMedicamentoEmBancoDados();

            Medicamento novoMedicamento1 = new()
            {
                Nome = "Decongeste",
                Descricao = "Bom para febre.",
                Lote = "123",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 12,
                Fornecedor = novoFornecedor1
            };
            repositorio.Inserir(novoMedicamento1);

            Medicamento novoMedicamento2 = new()
            {
                Nome = "Comprimido de vitamina",
                Descricao = "O que não mata, engorda.",
                Lote = "047",
                Validade = DateTime.Now.AddDays(20).Date,
                QuantidadeDisponivel = 100,
                Fornecedor = novoFornecedor2
            };
            repositorio.Inserir(novoMedicamento2);

            Medicamento novoMedicamento3 = new()
            {
                Nome = "Bandagem",
                Descricao = "Impede sangramento.",
                Lote = "893",
                Validade = DateTime.Now.AddDays(5).Date,
                QuantidadeDisponivel = 80,
                Fornecedor = novoFornecedor3
            };
            repositorio.Inserir(novoMedicamento3);

            var medicamentos = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, medicamentos.Count);

            Assert.AreEqual("Decongeste", medicamentos[0].Nome);
            Assert.AreEqual("Comprimido de vitamina", medicamentos[1].Nome);
            Assert.AreEqual("Bandagem", medicamentos[2].Nome);
        }
    }
}
