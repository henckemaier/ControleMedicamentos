﻿using ControleMedicamentos.Dominio.ModuloFuncionario;
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
    }
}
