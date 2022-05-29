using Shop.Core.WebApi;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApi.ViewModels;

public class CategoriaViewModel : EntityViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get;  set; }

    public IEnumerable<ProdutoViewModel> Produtos { get;  set; }
}
