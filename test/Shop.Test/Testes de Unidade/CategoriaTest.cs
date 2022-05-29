using Shop.Business.Models;
using Xunit;

namespace Shop.Test;

public class CategoriaTest
{
    [Fact(DisplayName = "Nome não ser nulo ou vário")]
    [Trait("Unitário", "Model Categoria")]
    public void Categoria_Nome_NaoDeveSerNuloOuVazio()
    {
        // Arrange
        var model = new Categoria("Teste");

        // Assert
        Assert.False(string.IsNullOrEmpty(model.Nome));
    }

    [Fact(DisplayName = "Nome não deve ser igual")]
    [Trait("Unitário", "Model Categoria")]
    public void Categoria_Nome_NaoDeveIgual()
    {
        // Arrange
        var model = new Categoria("Teste");

        // Act
        var nome = "Teste";

        // Assert
        Assert.Equal(nome, model.Nome);
    }

    [Fact(DisplayName = "Validar as informações")]
    [Trait("Unitário", "Model Categoria")]
    public void Categoria_Campo_DevemEstarValidado()
    {
        // Arrange
        var model = new Categoria("Teste");

        // Act
        var resultado = model.EhValido();

        // Assert
        Assert.True(resultado);
    }

    [Fact(DisplayName = "Validar as informações")]
    [Trait("Unitário", "Model Categoria")]
    public void Categoria_Campo_NaoDevemEstarValidado()
    {
        // Arrange
        var model = new Categoria("");

        // Act
        var resultado = model.EhValido();

        // Assert
        Assert.False(resultado);
    }
}
