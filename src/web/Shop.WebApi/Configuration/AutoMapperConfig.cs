using AutoMapper;
using Shop.Business.Models;
using Shop.WebApi.ViewModels;

namespace Shop.WebApi.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
        CreateMap<ProdutoViewModel, Produto>().ReverseMap();
    }
}
