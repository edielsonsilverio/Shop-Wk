using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shop.Business.Models;
using Shop.DataAccess.Context;
using Shop.Test.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Shop.Tests.Mapping;

public abstract class ParametroTesteConfig
{
    public static ShopDbContext ObterInstanciaContextoMock()
    {
        return CriarContext().Object;
    }

    public static ShopDbContext ObterInstanciaContextoInMemory(string databaseName)
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        var context = new ShopDbContext(options);
        return context;
    }

    private static Mock<ShopDbContext> CriarContext()
    {
        //Cria um objeto Context do entityFramework e configura o DbSet e Retorna os objetos
        //Chama a função para obter todos
        var data = ObterCategoria().AsQueryable();
        var dbSet = new Mock<DbSet<Categoria>>();
        dbSet.As<IQueryable<Categoria>>().Setup(x => x.Provider).Returns(data.Provider);
        dbSet.As<IQueryable<Categoria>>().Setup(x => x.Expression).Returns(data.Expression);
        dbSet.As<IQueryable<Categoria>>().Setup(x => x.ElementType).Returns(data.ElementType);
        dbSet.As<IQueryable<Categoria>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

        //Configuração para simular o retorno de uma lista.
        dbSet.As<IAsyncEnumerable<Categoria>>()
            .Setup(x => x.GetAsyncEnumerator(new CancellationToken()))
            .Returns(new AsyncEnumerator<Categoria>(data.GetEnumerator()));

        //Configurção simular a consulta com um filtro.
        dbSet.As<IQueryable<Categoria>>().Setup(x => x.Provider)
            .Returns(new AsyncQueryProvider<Categoria>(data.Provider));


        var context = new Mock<ShopDbContext>();
        context.Setup(x => x.Categorias).Returns(dbSet.Object);
        return context;
    }

    protected static IEnumerable<Categoria> ObterCategoria()
    {
        //Cria Dados Fakes para fazer os testes.
        GenFu.GenFu.Configure<Categoria>()
            .Fill(x => x.Nome).AsArticleTitle()
            .Fill(x => x.Id, () => { return Guid.NewGuid(); });


        var model = GenFu.GenFu.ListOf<Categoria>(30);
        model[0].Id = Guid.Empty;

        return model;
    }

    public static IMapper ObterMapperConfig()
    {
        return new MapperConfiguration(cfg => { cfg.AddProfile(new MappingTest()); })
            .CreateMapper();
    }
}
