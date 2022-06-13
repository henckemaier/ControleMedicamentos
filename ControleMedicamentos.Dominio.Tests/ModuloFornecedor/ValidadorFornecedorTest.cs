using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTest
    {
        public ValidadorFornecedorTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Fornecedor();
            f.Nome = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Fornecedor();
            p.Nome = "Rodovaldo";
            p.Telefone = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Telefone' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_do_fornecedor_deve_ser_no_minimo_11_caracteres()
        {
            //arrange
            var p = new Fornecedor();
            p.Nome = "Rodovaldo";
            p.Telefone = "3989047689";

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Telefone' deve ser maior ou igual a 11 caracteres. Você digitou 10 caracteres.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Fornecedor();
            p.Nome = "Rodovaldo";
            p.Telefone = "12345678901";
            p.Email = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Email' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Fornecedor();
            p.Nome = "Rodovaldo";
            p.Telefone = "12345678901";
            p.Email = "blablabla@gmail.com";
            p.Cidade = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Cidade' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_do_fornecedor_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Fornecedor();
            p.Nome = "Rodovaldo";
            p.Telefone = "12345678901";
            p.Email = "blablabla@gmail.com";
            p.Cidade = "Lages";
            p.Estado = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Estado' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }
    }
}