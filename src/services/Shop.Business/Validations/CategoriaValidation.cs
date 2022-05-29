using FluentValidation;
using Shop.Business.Models;

namespace Shop.Business.Validations;

public class CategoriaValidation : AbstractValidator<Categoria>
{
    public CategoriaValidation()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
