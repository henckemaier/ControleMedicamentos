using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                .NotNull().NotEmpty();

            RuleFor(x => x.Telefone)
                .MinimumLength(11)
                .NotNull().NotEmpty();

            RuleFor(x => x.Email)
                .NotNull().NotEmpty();

            RuleFor(x => x.Cidade)
                .NotNull().NotEmpty();

            RuleFor(x => x.Estado)
                .NotNull().NotEmpty();
        }
    }
}
