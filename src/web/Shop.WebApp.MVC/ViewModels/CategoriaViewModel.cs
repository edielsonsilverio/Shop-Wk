using System.ComponentModel.DataAnnotations;
using Shop.Core.WebApi;

namespace Shop.WebApp.MVC.ViewModels;

public class CategoriaViewModel : EntityViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
    public string Nome { get;  set; }
    public int? Status { get; set; }
    public bool Ativo { get; set; } = true;
    public ProdutoViewModel Veiculo { get;  set; }
}
