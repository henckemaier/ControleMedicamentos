using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class ValidadorRequisicaoTest
    {
        public ValidadorRequisicaoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Quantidade_de_medicamento_deve_ser_maior_que_0()
        {
            //arrange
            var r = new Requisicao();
            r.QtdMedicamento = 0;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("'Qtd Medicamento' deve ser superior a '0'.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Data_de_deve_ser_informada()
        {
            //arrange
            var r = new Requisicao();
            r.QtdMedicamento = 2;
            r.Data = DateTime.MinValue;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("'Data' deve ser informado.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Paciente_deve_ser_informado()
        {
            //arrange
            var r = new Requisicao();
            r.QtdMedicamento = 2;
            r.Data = DateTime.Now.Date;
            r.Paciente = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("'Paciente' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Medicamento_deve_ser_informado()
        {
            //arrange
            var c = new Paciente();
            c.Nome = "edu";
            c.CartaoSUS = "111111111111111";

            var r = new Requisicao();
            r.QtdMedicamento = 2;
            r.Data = DateTime.Now.Date;
            r.Paciente = c;
            r.Medicamento = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("'Medicamento' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Funcionario_deve_ser_informado()
        {
            //arrange
            var c = new Paciente();
            c.Nome = "edu";
            c.CartaoSUS = "111111111111111";

            var f = new Fornecedor();
            f.Nome = "gddfgdfg";
            f.Email = "sdfdsffdsffd";
            f.Telefone = "11111111111";
            f.Cidade = "Lages";
            f.Estado = "Santa Catarina";

            var m = new Medicamento();
            m.Nome = "dsfgfdgd";
            m.Descricao = "dfgdfgdfg";
            m.Lote = "234234234";
            m.QuantidadeDisponivel = 2;
            m.Validade = DateTime.Now.Date;
            m.Fornecedor = f;

            var r = new Requisicao();
            r.QtdMedicamento = 2;
            r.Data = DateTime.Now.Date;
            r.Paciente = c;
            r.Medicamento = m;
            r.Funcionario = null;

            ValidadorRequisicao validador = new ValidadorRequisicao();

            //action
            var resultado = validador.Validate(r);

            //assert
            Assert.AreEqual("'Funcionario' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }
    }
}
