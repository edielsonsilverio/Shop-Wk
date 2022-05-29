using Shop.Core.Notificacoes;

namespace Shop.Core.DomainObjects;

public interface INotificador
{
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Manipulador(Notificacao notificacao);
}