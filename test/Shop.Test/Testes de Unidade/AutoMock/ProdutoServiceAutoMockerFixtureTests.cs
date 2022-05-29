using Moq;
using Shop.Business.Intefaces;
using Xunit;

namespace Shop.Test;

[Collection(nameof(ProdutoAutoMockerCollection))]
public class ProdutoServiceAutoMockerFixtureTests
{
    readonly ProdutoTestsAutoMockerFixture _produtoTestsAutoMockerFixture;

    private readonly ProdutoService _produtoService;

    public ProdutoServiceAutoMockerFixtureTests(ProdutoTestsAutoMockerFixture produtoTestsFixture)
    {
        _produtoTestsAutoMockerFixture = produtoTestsFixture;
        _produtoService = _produtoTestsAutoMockerFixture.ObterProdutoService();
    }

    [Fact(DisplayName = "Adicionar Produto com Sucesso")]
    [Trait("Mock", "Produto Service AutoMockFixture Tests")]
    public async void ProdutoService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var produto = _produtoTestsAutoMockerFixture.GerarProdutoValido();

        // Act
        await _produtoService.Adicionar(produto);

        // Assert
        Assert.True(produto.Validar());
        _produtoTestsAutoMockerFixture.Mocker.GetMock<IProdutoRepository>().Verify(r => r.Adicionar(produto), Times.Never);

    }

    [Fact(DisplayName = "Adicionar Produto com Falha")]
    [Trait("Mock", "Produto Service AutoMockFixture Tests")]
    public async void ProdutoService_Adicionar_DeveFalharDevidoProdutoInvalido()
    {
        // Arrange
        var produto = _produtoTestsAutoMockerFixture.GerarProdutoInvalido();

        // Act
        await _produtoService.Adicionar(produto);

        // Assert
        Assert.True(produto.Validar());
        _produtoTestsAutoMockerFixture.Mocker.GetMock<IProdutoRepository>().Verify(r => r.Adicionar(produto), Times.Never);
        
    }
}