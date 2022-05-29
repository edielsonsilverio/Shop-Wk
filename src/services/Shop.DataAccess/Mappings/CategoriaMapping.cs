using Core.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Business.Models;

namespace Shop.DataAccess.Mappings;

public class CategoriaMapping : EntityMapping<Categoria>
{
    private static string _nameTable = nameof(Categoria).ToLower();
    public CategoriaMapping() : base(_nameTable) { }
    public override void Configure(EntityTypeBuilder<Categoria> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Nome).HasMaxLength(50);

        builder.HasData( 
            new Categoria("Roupa"),
            new Categoria("Brinquedo"),
            new Categoria("Sapato")
         );
    }
}
