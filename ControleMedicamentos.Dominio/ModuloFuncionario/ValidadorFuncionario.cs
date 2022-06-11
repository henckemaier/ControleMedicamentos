

using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario :AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty();

            RuleFor(x => x.Login)
                .NotNull().NotEmpty();

            RuleFor(x => x.Senha)
                .MinimumLength(5)
                .NotNull().NotEmpty();
        }
    }
}
