using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Domain.Model;
using AutoMapper;

namespace Autoglass.DesafioTecnico.Application.AutoMapper
{
    public class ProdutoAutoMapper : Profile
    {
        public ProdutoAutoMapper()
        {
            CreateMap<ProdutoRequestModel, Produto>()
                .ConstructUsing(x => new Produto(x.Descricao, x.DataFabricacao, x.DataValidade, x.CodigoFornecedor, x.DescricaoFornecedor, x.CNPJFornecedor))
                .ReverseMap();
        }
    }
}
