using AutoMapper;
using Shop.Business.Models;
using Shop.WebApi.ViewModels;

namespace Shop.Tests.Mapping;

public class MappingTest : Profile
{
    public MappingTest()
    {
        CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
    }
}