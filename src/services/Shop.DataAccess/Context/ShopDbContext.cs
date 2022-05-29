using Core.Data;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Shop.Business.Models;
using Shop.Core;
using Shop.Core.Data;
using Shop.Core.DomainObjects;
using Shop.Core.Mediator;
using Shop.Core.Messages;

namespace Shop.DataAccess.Context;

public class ShopDbContext : DbContext, IUnitOfWork
{
    //private readonly IMediatorHandler _mediatorHandler;

    //public ShopDbContext() { }
    public ShopDbContext(DbContextOptions<ShopDbContext> options//,IMediatorHandler mediatorHandler
                            ) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;

        //_mediatorHandler = mediatorHandler;
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigurarConvencaoTipoColuna(TipoServidorBancoDados.SQLServer);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ////optionsBuilder.ConfigurarConexao(TipoServidorBancoDados.SQLServer, GlobalConstants.CONEXAO_BANCO_SQLSERVER);
        //var conn = "Server = 127.0.0.1;Port=3306; Database = shop_test; Uid = root; Pwd = estadao";
        //optionsBuilder.UseMySql(ServerVersion.AutoDetect(conn));


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurarRelacionamentoEntidades(DeleteBehavior.ClientSetNull);

        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("DataCadastro").IsModified = false;
        }
        try
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            //if (sucesso) await _mediatorHandler.PublicarEventos(this);
            return sucesso;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            return false;
        }

    }
}

public static class MediatorExtension
{
    public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await mediator.PublicarEvento(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}

/*
         Add-Migration TabelaIniciais -Project src\Web\Shop.WebApi -Context ApplicationDbContext -StartupProject src\Web\Shop.WebApi

         Update-Database -StartupProject src\Web\Shop.WebApi -Project src\Web\Shop.WebApi -Context ApplicationDbContext

 
 Add-Migration TabelaInicias -Project src\DataAccess\Shop.DataAccess -StartupProject src\DataAccess\Shop.DataAccess -Context ShopDbContext
 Update-Database -Project src\DataAccess\Shop.DataAccess -StartupProject src\DataAccess\Shop.DataAccess -Context ShopDbContext
 
 */
