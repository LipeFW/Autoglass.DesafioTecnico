using AutoFixture;
using Autoglass.DesafioTecnico.Application.AutoMapper;
using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Application.Service;
using Autoglass.DesafioTecnico.Domain.Model;
using Autoglass.DesafioTecnico.Infrastructure.Repository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autoglass.DesafioTecnico.Test.Application.Service
{
    [TestClass]
    public class ProdutoServiceTest
    {
        private ProdutoService _produtoService;
        private Mock<ProdutoRepository> _mockProdutoRepository;
        private IMapper _mapper;
        private Fixture _fixture;

        [TestInitialize]
        public void Init()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProdutoAutoMapper>();
            }));

            _mockProdutoRepository = new Mock<ProdutoRepository>(null);
            _produtoService = new ProdutoService(_mockProdutoRepository.Object, _mapper);
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Should_Post_Create_Produto_Valido()
        {
            var mockRequest = _fixture.Build<ProdutoRequestModel>()
                .With(x => x.DataFabricacao, new DateTime(2023, 03, 23))
                .With(x => x.DataValidade, new DateTime(2023, 03, 25))
                .Create();


            _mockProdutoRepository.Setup(x => x.Post(It.IsAny<Produto>())).Returns(1);

            var result = _produtoService.Post(mockRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Post_Throw_ArgumentException_When_Descricao_Null()
        {
            var mockRequest = _fixture.Build<ProdutoRequestModel>()
                .With(x => x.Descricao, "")
                .Create();

            _mockProdutoRepository.Setup(x => x.Post(It.IsAny<Produto>())).Returns(1);

            var result = _produtoService.Post(mockRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Post_Throw_ArgumentException_When_Data_Invalid()
        {
            var mockRequest = _fixture.Build<ProdutoRequestModel>()
                .With(x => x.DataFabricacao, new DateTime(2023, 03, 28))
                .With(x => x.DataValidade, new DateTime(2023, 03, 25))
                .Create();

            _mockProdutoRepository.Setup(x => x.Post(It.IsAny<Produto>())).Returns(1);

            var result = _produtoService.Post(mockRequest);
        }

        [TestMethod]
        public void Should_Disable_Produto_Return_False()
        {
            _produtoService.Delete(1);

            _mockProdutoRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Should_GetById_Return_Produto()
        {
            var mockResponse = _fixture.Create<Produto>();

            _mockProdutoRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockResponse);

            var result = _produtoService.GetById(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Should_GetById_Return_Null()
        {
            var result = _produtoService.GetById(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Should_GetAll_Return_All_Produtos()
        {
            var mockResponse = _fixture.Create<List<Produto>>().AsQueryable();

            _mockProdutoRepository.Setup(x => x.GetAll()).Returns(mockResponse);

            var result = _produtoService.GetAll(new GetAllProdutoRequestModel());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TotalItems > 0);
            Assert.IsTrue(result.Data.Count > 0);
        }

        [TestMethod]
        public void Should_GetAll_Return_Nothing()
        {
            var result = _produtoService.GetAll(new GetAllProdutoRequestModel());

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.TotalItems);
            Assert.AreEqual(0, result.Data.Count);
        }

        [TestMethod]
        public void Should_Patch_Update_Produto_Properties()
        {
            var mockRequest = new JsonPatchDocument<Produto>();

            var mockProduto = _fixture.Create<Produto>();

            _mockProdutoRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockProduto);

            _produtoService.Patch(1, mockRequest);

            _mockProdutoRepository.Verify(x => x.Patch(It.IsAny<Produto>()), Times.Once);
        }
    }
}
