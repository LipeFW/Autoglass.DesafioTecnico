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

            CreateMap<Produto, ProdutoResponseModel>()
                .ForMember(x => x.Descricao, opt => opt.MapFrom(x => x.Descricao ?? ""))
                .ForMember(x => x.Situacao, opt => opt.ConvertUsing(new DateTimeTypeConverter()))
                .ReverseMap();
        }

        public class DateTimeTypeConverter : IValueConverter<bool, string>
        {
            public string Convert(bool sourceMember, ResolutionContext context) =>
                sourceMember ? "Ativo" : "Inativo";
        }
    }
}
