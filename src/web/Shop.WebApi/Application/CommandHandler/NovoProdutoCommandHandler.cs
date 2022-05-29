using MediatR;
using Shop.Business.Intefaces;
using Shop.Business.Models;
using Shop.Business.Validations;
using Shop.MessageBus.BusRabbit;
using Shop.MessageBus.EventQueue;
using Shop.WebApi.Application.Commands;

namespace Shop.WebApi.Application.CommandHandler
{
    public class NovoProdutoCommandHandler : IRequestHandler<NovoProdutoCommand, bool>
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IRabbitEventBus _rabbitBus;

        public NovoProdutoCommandHandler(
            IProdutoService produtoService,
            IRabbitEventBus rabbitBus,
            IProdutoRepository produtoRepository)
        {
            _produtoService = produtoService;
            _rabbitBus = rabbitBus;
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> Handle(NovoProdutoCommand message, CancellationToken cancellationToken)
        {
            var model = new Produto(message.Nome, message.Descricao, message.ValorCompra, message.ValorVenda,
                message.EstoqueMinimo,
                message.EstoqueMaximo);

            model.CategoriaId = message.CategoriaId;
            model.QuantidadeEstoque = message.QuantidadeEstoque;


            var validation = new ProdutoValidation();
            if (!validation.Validate(model).IsValid) return false;

            var result = await _produtoService.Adicionar(model);

            if (!result) throw new Exception("Não foi possível inserir o Produto.");

            await _produtoRepository.PersistirDados();

            //Envia a mensagem
            _rabbitBus.Publish(new EmailEventQueue(
                "desitnario@teste.com",
                 message.Nome,
                "Produto Cadastrado com Sucesso"
            ));

            return result;
        }


    }
}
