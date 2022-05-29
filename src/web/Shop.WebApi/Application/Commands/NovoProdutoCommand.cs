using MediatR;

namespace Shop.WebApi.Application.Commands
{
    public class NovoProdutoCommand : IRequest<bool>
    {
          public string Nome { get; set; }
        public string Descricao { get; set; }

        public decimal ValorCompra { get; set; }

        public decimal ValorVenda { get; set; }

        public decimal EstoqueMinimo { get; set; }
        public decimal EstoqueMaximo { get; set; }
        public decimal QuantidadeEstoque { get; set; }

        public Guid CategoriaId { get; set; }
    }
}
