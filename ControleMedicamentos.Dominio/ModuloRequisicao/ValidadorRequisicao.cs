using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.QtdMedicamento)
                .GreaterThan(0)
                .NotNull().NotEmpty();

            RuleFor(x => x.Data)
                .NotNull().NotEmpty();

            RuleFor(x => x.Paciente)
                .NotNull().NotEmpty();

            RuleFor(x => x.Medicamento)
                .NotNull().NotEmpty();

            RuleFor(x => x.Funcionario)
                .NotNull().NotEmpty();
        }
    }
}
