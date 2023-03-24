using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Domain.Model;
using AutoMapper;
using System.Text.RegularExpressions;

namespace Autoglass.DesafioTecnico.Application.AutoMapper
{
    public class ProdutoAutoMapper : Profile
    {
        public ProdutoAutoMapper()
        {
            CreateMap<ProdutoRequestModel, Produto>()
                .ConstructUsing(x => new Produto(x.Descricao, x.DataFabricacao, x.DataValidade, x.CodigoFornecedor, x.DescricaoFornecedor,x.CNPJFornecedor))
                .ReverseMap();

            CreateMap<Produto, ProdutoResponseModel>()
                .ForMember(x => x.Descricao, opt => opt.MapFrom(x => x.Descricao ?? ""))
                .ForMember(x => x.Situacao, opt => opt.ConvertUsing(new DateTimeTypeConverter()))
                .ForMember(x => x.DescricaoFornecedor, opt => opt.MapFrom(x => x.DescricaoFornecedor ?? ""))
                .ForMember(x => x.CNPJFornecedor, opt => opt.MapFrom(x => x.CNPJFornecedor ?? ""))
                .ForMember(x => x.DataValidade, opt => opt.MapFrom(x => x.DataValidade))
                .ForMember(x => x.DataFabricacao, opt => opt.MapFrom(x => x.DataFabricacao))
                .ForMember(x => x.Codigo, opt => opt.MapFrom(x => x.Codigo))
                .ForMember(x => x.CodigoFornecedor, opt => opt.MapFrom(x => x.CodigoFornecedor))
                .ReverseMap();
        }

        public class DateTimeTypeConverter : IValueConverter<bool, string>
        {
            public string Convert(bool sourceMember, ResolutionContext context) =>
                sourceMember ? "Ativo" : "Inativo";
        }
    }
}
