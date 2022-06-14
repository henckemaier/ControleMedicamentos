using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTest
    {
        public ValidadorMedicamentoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Decongeste";
            m.Descricao = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("'Descricao' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Decongeste";
            m.Descricao = "Alivia a febre";
            m.Lote = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("'Lote' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Validade_do_medicamento_deve_ser_obrigatorio()
        {
            //arrange
            var m = new Medicamento();
            m.Nome = "Decongeste";
            m.Descricao = "Alivia a febre";
            m.Lote = "123";
            m.Validade = DateTime.MinValue;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            //action
            var resultado = validador.Validate(m);

            //assert
            Assert.AreEqual("'Validade' deve ser informado.", resultado.Errors[0].ErrorMessage);
        }
    }
}
