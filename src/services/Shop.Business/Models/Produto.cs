using Shop.Business.Validations;
using Shop.Core.DomainObjects;

namespace Shop.Business.Models;

public class Produto : Entity, IAggregateRoot
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal ValorCompra { get; set; }
    public decimal ValorVenda { get; set; }
    public decimal EstoqueMinimo { get; set; }
    public decimal EstoqueMaximo { get; set; }
    public decimal QuantidadeEstoque { get; set; }
    public DateTime? DataCadastro { get; set; }
    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; }


    // EF Relation
    protected Produto() { }

    public Produto(string nome, string descricao, decimal valorCompra, decimal valorVenda, decimal estoqueMinimo, decimal estoqueMaximo)
    {
        Nome = nome;
        Descricao = descricao;
        ValorCompra = valorCompra;
        ValorVenda = valorVenda;
        EstoqueMinimo = estoqueMinimo;
        EstoqueMaximo = estoqueMaximo;
    }

    public bool Validar()
    {
        try
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do proprietario não pode estar vazio");
            Validacoes.ValidarSeVazio(Descricao, "O campo Descriçaão não pode estar vazio");
            Validacoes.ValidarSeMenorQue(ValorCompra, 1, "O campo Valor de Compra não pode se menor igual a 0");
            Validacoes.ValidarSeMenorQue(ValorVenda, ValorCompra, "O campo Valor de Venda não pode se menor que o Valor de Compra");
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public override bool EhValido()
    {
        return new ProdutoValidation().Validate(this).IsValid;
    }
}
