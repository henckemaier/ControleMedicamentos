using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTest
    {
        public ValidadorPacienteTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_paciente_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Paciente();
            p.Nome = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_do_paciente_deve_ser_obrigatorio()
        {
            //arrange
            var p = new Paciente();
            p.Nome = "Rodovaldo";
            p.CartaoSUS = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Cartao SUS' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_do_paciente_deve_ser_no_minimo_15_caracteres()
        {
            //arrange
            var p = new Paciente();
            p.Nome = "Rodovaldo";
            p.CartaoSUS = "34534";

            ValidadorPaciente validador = new ValidadorPaciente();

            //action
            var resultado = validador.Validate(p);

            //assert
            Assert.AreEqual("'Cartao SUS' deve ser maior ou igual a 15 caracteres. Você digitou 5 caracteres.", resultado.Errors[0].ErrorMessage);
        }
    }
}
