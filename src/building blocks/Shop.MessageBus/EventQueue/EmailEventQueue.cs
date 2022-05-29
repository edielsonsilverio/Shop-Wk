using Shop.MessageBus.Events;

namespace Shop.MessageBus.EventQueue;

public class EmailEventQueue : Event
{
    public EmailEventQueue(string destinatario, string titulo, string conteudo)
    {
        Destinatario = destinatario;
        Titulo = titulo;
        Conteudo = conteudo;
    }

    public string Destinatario { get; set; }
    public string Titulo { get; set; }
    public string Conteudo { get; set; }


}
