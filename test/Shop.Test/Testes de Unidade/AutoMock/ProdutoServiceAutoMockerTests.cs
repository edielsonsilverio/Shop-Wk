using Moq;
using Moq.AutoMock;
using Shop.Business.Intefaces;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Test;

[Collection(nameof(ProdutoAutoMockerCollection))]
public class ProdutoServiceAutoMockerTests
{
    readonly ProdutoTestsAutoMockerFixture _produtoTestsBogus;

    public ProdutoServiceAutoMockerTests(ProdutoTestsAutoMockerFixture produtoTestsFixture)
    {
        _produtoTestsBogus = produtoTestsFixture;

    }

    [Fact(DisplayName = "Adicionar Produto com Sucesso")]
    [Trait("Mock", "Produto Service AutoMock Tests")]
    public async Task ProdutoService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var produto = _produtoTestsBogus.GerarProdutoValido();
        var mocker = new AutoMocker();
        var produtoService = mocker.CreateInstance<ProdutoService>();

        // Act
        await produtoService.Adicionar(produto);

        // Assert
        Assert.True(produto.Validar());
        mocker.GetMock<IProdutoRepository>().Verify(r => r.Adicionar(produto), Times.Never);
    }

    [Fact(DisplayName = "Adicionar Produto com Falha")]
    [Trait("Mock", "Produto Service AutoMock Tests")]
    public async Task ProdutoService_Adicionar_DeveFalharDevidoProdutoInvalido()
    {
        // Arrange
        var produto = _produtoTestsBogus.GerarProdutoInvalido();
        var mocker = new AutoMocker();
        var produtoService = mocker.CreateInstance<ProdutoService>();

        // Act
       await produtoService.Adicionar(produto);

        // Assert
        Assert.False(produto.EhValido());
        mocker.GetMock<IProdutoRepository>().Verify(r => r.Adicionar(produto), Times.Never);
    }
 
}
