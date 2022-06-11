using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTest
    {
        public ValidadorFuncionarioTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Funcionario();
            f.Nome = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Login_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Funcionario();
            f.Nome = "Rodovaldo";
            f.Login = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("'Login' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ser_obrigatorio()
        {
            //arrange
            var f = new Funcionario();
            f.Nome = "Rodovaldo";
            f.Login = "RodovaldoTB2021";
            f.Senha = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("'Senha' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ser_minimo_5_catacteres()
        {
            //arrange
            var f = new Funcionario();
            f.Nome = "Rodovaldo";
            f.Login = "RodovaldoTB2021";
            f.Senha = "1234";

            ValidadorFuncionario validador = new ValidadorFuncionario();

            //action
            var resultado = validador.Validate(f);

            //assert
            Assert.AreEqual("'Senha' deve ser maior ou igual a 5 caracteres. Você digitou 4 caracteres.", resultado.Errors[0].ErrorMessage);
        }
    }
}
