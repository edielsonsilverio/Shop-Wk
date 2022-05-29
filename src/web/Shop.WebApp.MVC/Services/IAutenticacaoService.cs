using Shop.WebApp.MVC.ViewModels;

namespace Shop.WebApp.MVC.Services;
public interface IAutenticacaoService
{
    Task<LoginResponseViewModel> Login(LoginUserViewModel usuarioLogin);
    Task<LoginResponseViewModel> Registro(RegisterUserViewModel usuarioRegistro);
    Task RealizarLogin(LoginResponseViewModel resposta);
    Task Logout();
}

