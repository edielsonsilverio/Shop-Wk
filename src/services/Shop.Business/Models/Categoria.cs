using Shop.Business.Validations;
using Shop.Core.DomainObjects;

namespace Shop.Business.Models;

public  class Categoria : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public DateTime DataCadastro { get; private set; }

    // EF Relation
    public IEnumerable<Produto> Produtos { get; protected set; }
    public Categoria() { }

    public Categoria(string nome)
    {
        Nome = nome;
        DataCadastro = DateTime.Now;
        Validar();
    }

    public bool Validar()
    {
        try
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome da marca não pode estar vazio");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public override bool EhValido()
    {
        return new CategoriaValidation().Validate(this).IsValid;
    }
}
