using Shop.Business.Models;
using Shop.Tests.Mapping;
using System;
using System.Linq;
using Xunit;

namespace Shop.Tests;

public class CategoriaServiceTest : ParametroTesteConfig
{

    
    [Fact(DisplayName = "Adicionar uma Categoria")]
    [Trait("Unitário", "Serviço Categoria")]
    public async void Categoria_Adicionar_NovoCategoria()
    {
        //Arrange
        var context = ObterInstanciaContextoInMemory("BaseCategoriaAdicionar");

        var model = new Categoria("Teste");

        //Act
        context.Add(model);
        var result = context.SaveChanges() > 0?true:false;

        //Assert
        Assert.True(result);
    }

    [Fact(DisplayName = "Atualizar uma Categoria")]
    [Trait("Unitário", "Serviço Categoria")]
    public async void Categoria_Adicionar_AtualziarCategoria()
    {
        //Arrange
        var context = ObterInstanciaContextoInMemory("BaseCategoriaAdicionar");

        var model = new Categoria("Teste");
        model.Id = Guid.Empty;

        //Act
        context.Update(model);
        var result = context.SaveChanges() > 0 ? true : false;

        //Assert
        Assert.True(result);
    }

}
