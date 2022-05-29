using Core.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Business.Models;

namespace Shop.DataAccess.Mappings;

public class ProdutoMapping : EntityMapping<Produto>
{
    private static string _nameTable = nameof(Produto).ToLower();
    public ProdutoMapping() : base(_nameTable) { }
    public override void Configure(EntityTypeBuilder<Produto> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Descricao).HasMaxLength(50).IsRequired();
        builder.Property(x => x.EstoqueMinimo);
        builder.Property(x => x.EstoqueMaximo);
        builder.Property(x => x.QuantidadeEstoque);
        builder.Property(x => x.ValorCompra).HasPrecision(18,2);
        builder.Property(x => x.ValorVenda).HasPrecision(18,2);

        // 1 : N => Cateogira : Produto
        builder.HasOne(c => c.Categoria)
            .WithMany(p => p.Produtos)
            .HasForeignKey(c => c.CategoriaId);
        

    }
}