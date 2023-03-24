using AutoFixture;
using Autoglass.DesafioTecnico.Api.Controllers;
using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Application.Service;
using Autoglass.DesafioTecnico.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Autoglass.DesafioTecnico.Test.Api.Controllers
{
    [TestClass]
    public class ProdutosControllerTest
    {
        private ProdutosController _produtosController;
        private Mock<ProdutoService> _mockProdutoService;
        private Fixture _fixture;

        [TestInitialize]
        public void Init()
        {
            _mockProdutoService = new Mock<ProdutoService>(null, null);
            _produtosController = new ProdutosController(_mockProdutoService.Object);

            _produtosController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            };

            _fixture = new Fixture();
        }

        [TestMethod]
        public void Should_GetAll_Return_OkObjectResult()
        {
            var mockListProdutoResponse = _fixture.Create<GetAllProdutoResponseModel>();

            _mockProdutoService.Setup(x => x.GetAll(It.IsAny<GetAllProdutoRequestModel>())).Returns(mockListProdutoResponse);

            var request = _fixture.Create<GetAllProdutoRequestModel>();

            var response = _produtosController.GetAll(request);

            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));
        }

        [TestMethod]
        public void Should_GetById_Return_OkObjectResult()
        {
            var mockProduto = _fixture.Create<ProdutoResponseModel>();

            _mockProdutoService.Setup(x => x.GetById(1)).Returns(mockProduto);

            var response = _produtosController.GetById(1);

            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));
        }

        [TestMethod]
        public void Should_GetById_Return_NotFoundResult_When_Produto_Doesnt_Exists()
        {
            var response = _produtosController.GetById(1);

            Assert.AreEqual(response.GetType(), typeof(NotFoundResult));
        }

        [TestMethod]
        public void Should_Post_Return_Created()
        {
            var mockRequest = _fixture.Create<ProdutoRequestModel>();

            var response = _produtosController.Post(mockRequest);

            Assert.AreEqual(response.GetType(), typeof(CreatedResult));
        }

        [TestMethod]
        public void Should_Post_Return_BadRequest()
        {
            var mockRequest = _fixture.Build<ProdutoRequestModel>()
                .With(x => x.DataFabricacao, new DateTime(2023, 03, 23))
                .With(x => x.DataValidade, new DateTime(2023, 03, 22))
                .Create();

            _mockProdutoService.Setup(x => x.Post(mockRequest)).Throws(new ArgumentException());

            var response = _produtosController.Post(mockRequest);

            Assert.AreEqual(response.GetType(), typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Should_Delete_Return_NoContent()
        {
            var response = _produtosController.Delete(1);

            Assert.AreEqual(response.GetType(), typeof(NoContentResult));
        }

        [TestMethod]
        public void Should_Patch_Return_OkResult()
        {
            var mockRequest = new JsonPatchDocument<Produto>();

            var response = _produtosController.Patch(1, mockRequest);

            Assert.AreEqual(response.GetType(), typeof(OkResult));
        }
    }
}
