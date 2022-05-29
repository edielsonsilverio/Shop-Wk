using Shop.Business.Models;
using Xunit;

namespace Shop.Test;

public class ProdutoTest
{
    [Fact(DisplayName = "Os Campos devem ser Obrigatórios")]
    [Trait("Unitário", "Model Produto")]
    public void Produto_Validacao_CamposDevemSerObrigatorio()
    {
        // Arrange
        var model = new Produto("Teste", "Teste", 100,500,10,150);

        // Act & Assert
        var result = model.Validar();

        // Assert 
        Assert.True(result);
    }

    [Fact(DisplayName = "Nome não deve ser igual")]
    [Trait("Unitário", "Model Produto")]
    public void Produto_Nome_NaoDeveIgual()
    {
        // Arrange
        var model = new Produto("Teste", "Teste", 100, 500, 10, 150);

        // Act
        var nome = "Teste";

        // Assert
        Assert.Equal(nome, model.Nome);
    }

}