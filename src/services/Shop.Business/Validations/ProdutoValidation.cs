using FluentValidation;
using Shop.Business.Models;

namespace Shop.Business.Validations;

public class ProdutoValidation : AbstractValidator<Produto>
{
    public ProdutoValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(x => x.CategoriaId)
            .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} precisa ser fornecido");

        RuleFor(x => x.ValorCompra)
            .GreaterThan(-1).WithMessage("O campo {PropertyName} precicsa ser maior ou igual que zero")
            .WithName("Valor de Compra");

        RuleFor(x => x.ValorVenda)
            .GreaterThan(-1).WithMessage("O campo {PropertyName} precicsa ser maior ou igual que zero")
            .WithName("Valor de Venda");

    }
}
