using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
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

            /////////////////////////////////

            Paciente novoPaciente = new()
            {
                Nome = "Lucas",
                CartaoSUS = "123456789012345"
            };

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(novoPaciente);

            /////////////////////////////////

            Fornecedor novoFornecedor = new()
            {
                Nome = "Lucas",
                Telefone = "11111111111",
                Email = "blabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();

            repositorioFornecedor.Inserir(novoFornecedor);

            ////////////////////////////////

            Medicamento novoMedicamento = new()
            {
                Nome = "Lucas",
                Descricao = "asdagvfgdgdfgdf",
                Lote = "12345",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 10,
                Fornecedor = novoFornecedor
            };

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(novoMedicamento);

            ////////////////////////////////

            Requisicao novoRequisicao = new()
            {
                Funcionario = novoFuncionario,
                Paciente = novoPaciente,
                Medicamento = novoMedicamento,
                QtdMedicamento = 5,
                Data = DateTime.Now.Date
            };

            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            repositorioRequisicao.Inserir(novoRequisicao);

            //assert
            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(novoRequisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(novoRequisicao.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(novoRequisicao.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);
            Assert.AreEqual(novoRequisicao.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(novoRequisicao.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
            Assert.AreEqual(novoRequisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
            Assert.AreEqual(novoRequisicao.Data, requisicaoEncontrada.Data);
        }

        [TestMethod]
        public void Deve_editar_requisicao()
        {
            Funcionario novoFuncionario = new()
            {
                Nome = "Edu",
                Login = "edu2345",
                Senha = "1234567"
            };

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();

            repositorioFuncionario.Inserir(novoFuncionario);

            /////////////////////////////////

            Paciente novoPaciente = new()
            {
                Nome = "Lucas",
                CartaoSUS = "123456789012345"
            };

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(novoPaciente);

            /////////////////////////////////

            Fornecedor novoFornecedor = new()
            {
                Nome = "Lucas",
                Telefone = "11111111111",
                Email = "blabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();

            repositorioFornecedor.Inserir(novoFornecedor);

            ////////////////////////////////

            Medicamento novoMedicamento = new()
            {
                Nome = "Lucas",
                Descricao = "asdagvfgdgdfgdf",
                Lote = "12345",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 10,
                Fornecedor = novoFornecedor
            };

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(novoMedicamento);

            ////////////////////////////////

            Requisicao novoRequisicao = new()
            {
                Funcionario = novoFuncionario,
                Paciente = novoPaciente,
                Medicamento = novoMedicamento,
                QtdMedicamento = 5,
                Data = DateTime.Now.Date
            };

            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            repositorioRequisicao.Inserir(novoRequisicao);

            Requisicao requisicaoAtualizada = repositorioRequisicao.SelecionarPorNumero(novoMedicamento.Id);
            requisicaoAtualizada.Funcionario = novoFuncionario;
            requisicaoAtualizada.Paciente = novoPaciente;
            requisicaoAtualizada.Medicamento = novoMedicamento;
            requisicaoAtualizada.QtdMedicamento = 5;
            requisicaoAtualizada.Data = DateTime.Now.Date;

            //action
            repositorioRequisicao.Editar(requisicaoAtualizada);

            //assert
            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(novoRequisicao.Id);

            Assert.IsNotNull(requisicaoEncontrada);
            Assert.AreEqual(novoRequisicao.Id, requisicaoEncontrada.Id);
            Assert.AreEqual(novoRequisicao.Funcionario.Id, requisicaoEncontrada.Funcionario.Id);
            Assert.AreEqual(novoRequisicao.Paciente.Id, requisicaoEncontrada.Paciente.Id);
            Assert.AreEqual(novoRequisicao.Medicamento.Id, requisicaoEncontrada.Medicamento.Id);
            Assert.AreEqual(novoRequisicao.QtdMedicamento, requisicaoEncontrada.QtdMedicamento);
            Assert.AreEqual(novoRequisicao.Data, requisicaoEncontrada.Data);
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            Funcionario novoFuncionario = new()
            {
                Nome = "Edu",
                Login = "edu2345",
                Senha = "1234567"
            };

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();

            repositorioFuncionario.Inserir(novoFuncionario);

            /////////////////////////////////

            Paciente novoPaciente = new()
            {
                Nome = "Lucas",
                CartaoSUS = "123456789012345"
            };

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(novoPaciente);

            /////////////////////////////////

            Fornecedor novoFornecedor = new()
            {
                Nome = "Lucas",
                Telefone = "11111111111",
                Email = "blabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();

            repositorioFornecedor.Inserir(novoFornecedor);

            ////////////////////////////////

            Medicamento novoMedicamento = new()
            {
                Nome = "Lucas",
                Descricao = "asdagvfgdgdfgdf",
                Lote = "12345",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 10,
                Fornecedor = novoFornecedor
            };

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(novoMedicamento);

            ////////////////////////////////

            Requisicao novoRequisicao = new()
            {
                Funcionario = novoFuncionario,
                Paciente = novoPaciente,
                Medicamento = novoMedicamento,
                QtdMedicamento = 5,
                Data = DateTime.Now.Date
            };

            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            repositorioRequisicao.Inserir(novoRequisicao);

            //assert
            Requisicao requisicaoEncontrada = repositorioRequisicao.SelecionarPorNumero(novoRequisicao.Id);

            //action
            repositorioRequisicao.Excluir(requisicaoEncontrada);

            //assert
            Assert.IsNotNull(requisicaoEncontrada);
        }
        [TestMethod]
        public void Deve_selecionar_todas_requisicoes()
        {
            Funcionario novoFuncionario = new()
            {
                Nome = "Edu",
                Login = "edu2345",
                Senha = "1234567"
            };

            var repositorioFuncionario = new RepositorioFuncionarioEmBancoDados();

            repositorioFuncionario.Inserir(novoFuncionario);

            /////////////////////////////////

            Paciente novoPaciente = new()
            {
                Nome = "Lucas",
                CartaoSUS = "123456789012345"
            };

            var repositorioPaciente = new RepositorioPacienteEmBancoDeDados();

            repositorioPaciente.Inserir(novoPaciente);

            /////////////////////////////////

            Fornecedor novoFornecedor = new()
            {
                Nome = "Lucas",
                Telefone = "11111111111",
                Email = "blabla@gmail.com",
                Cidade = "Lages",
                Estado = "Santa Catarina"
            };

            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();

            repositorioFornecedor.Inserir(novoFornecedor);

            ////////////////////////////////

            Medicamento novoMedicamento = new()
            {
                Nome = "Lucas",
                Descricao = "asdagvfgdgdfgdf",
                Lote = "12345",
                Validade = DateTime.Now.AddDays(30).Date,
                QuantidadeDisponivel = 10,
                Fornecedor = novoFornecedor
            };

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            repositorioMedicamento.Inserir(novoMedicamento);

            ////////////////////////////////

            Requisicao novoRequisicao1 = new()
            {
                Funcionario = novoFuncionario,
                Paciente = novoPaciente,
                Medicamento = novoMedicamento,
                QtdMedicamento = 5,
                Data = DateTime.Now.Date
            };

            var repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            repositorioRequisicao.Inserir(novoRequisicao1);

            ////////////////////////////////

            Requisicao novoRequisicao2 = new()
            {
                Funcionario = novoFuncionario,
                Paciente = novoPaciente,
                Medicamento = novoMedicamento,
                QtdMedicamento = 3,
                Data = DateTime.Now.Date
            };

            repositorioRequisicao.Inserir(novoRequisicao2);

            ////////////////////////////////

            Requisicao novoRequisicao3 = new()
            {
                Funcionario = novoFuncionario,
                Paciente = novoPaciente,
                Medicamento = novoMedicamento,
                QtdMedicamento = 10,
                Data = DateTime.Now.Date
            };

            repositorioRequisicao.Inserir(novoRequisicao3);

            var requisicoes = repositorioRequisicao.SelecionarTodos();

            //assert
            Assert.AreEqual(3, requisicoes.Count);

            Assert.AreEqual(5, requisicoes[0].QtdMedicamento);
            Assert.AreEqual(3, requisicoes[1].QtdMedicamento);
            Assert.AreEqual(10, requisicoes[2].QtdMedicamento);


        }
    }
}
