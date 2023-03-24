using AutoFixture;
using Autoglass.DesafioTecnico.Application.AutoMapper;
using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Domain.Model;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Autoglass.DesafioTecnico.Test.Application.AutoMapper
{
    [TestClass]
    public class ProdutoAutoMapperTest
    {
        private IMapper _mapper;
        private Fixture _fixture;

        [TestInitialize]
        public void Init()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProdutoAutoMapper>();
            }));

            _fixture = new Fixture();
        }

        [TestMethod]
        public void Should_ProdutoAutoMapper_Return_ProdutoResponseModel()
        {
            var produtoEntity = _fixture.Create<Produto>();

            var produtoResponse = _mapper.Map<ProdutoResponseModel>(produtoEntity);

            Assert.AreEqual(produtoEntity.Codigo, produtoResponse.Codigo);
            Assert.AreEqual(produtoEntity.Descricao, produtoResponse.Descricao);
            Assert.AreEqual(produtoEntity.DataFabricacao, produtoResponse.DataFabricacao);
            Assert.AreEqual(produtoEntity.DataValidade, produtoResponse.DataValidade);
            Assert.AreEqual(produtoEntity.CNPJFornecedor, produtoResponse.CNPJFornecedor);
            Assert.AreEqual(produtoEntity.CodigoFornecedor, produtoResponse.CodigoFornecedor);
            Assert.AreEqual(produtoEntity.DescricaoFornecedor, produtoResponse.DescricaoFornecedor);
        }

        [TestMethod]
        public void Should_ProdutoAutoMapper_Return_Produto()
        {
            var produtoRequest = _fixture.Create<ProdutoRequestModel>();

            var produtoEntity = _mapper.Map<Produto>(produtoRequest);

            Assert.AreEqual(produtoRequest.Descricao, produtoEntity.Descricao);
            Assert.AreEqual(produtoRequest.DataFabricacao, produtoEntity.DataFabricacao);
            Assert.AreEqual(produtoRequest.DataValidade, produtoEntity.DataValidade);
            Assert.AreEqual(produtoRequest.CNPJFornecedor, produtoEntity.CNPJFornecedor);
            Assert.AreEqual(produtoRequest.CodigoFornecedor, produtoEntity.CodigoFornecedor);
            Assert.AreEqual(produtoRequest.DescricaoFornecedor, produtoEntity.DescricaoFornecedor);
        }
    }
}
