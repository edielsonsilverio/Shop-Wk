using Shop.Core.WebApi;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApi.ViewModels;

public class ProdutoViewModel : EntityViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }

    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal ValorCompra { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal ValorVenda { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal EstoqueMinimo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal EstoqueMaximo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal QuantidadeEstoque { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid CategoriaId { get; set; }
    public CategoriaViewModel Categoria { get; set; }
}
