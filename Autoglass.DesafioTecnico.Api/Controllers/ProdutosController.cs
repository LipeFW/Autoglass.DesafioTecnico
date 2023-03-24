using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Application.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Autoglass.DesafioTecnico.Api.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private ProdutoService _produtoService;

        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Cadastra um novo produto.
        /// </summary>
        /// <param name="request">   Corpo da requisição a ser preenchido com os dados do produto.
        /// 
        /// Exemplo:
        /// POST api/Produtos
        ///     {
        ///         "descricao" : "Breve descrição do produto",
        ///         "dataFabricacao" : "2023-03-23T20:52:14.161Z",
        ///         "dataValidade" : "2023-03-24T20:52:14.161Z",
        ///         "codigoFornecedor" : 1,
        ///         "descricaoFornecedor" : "Breve descrição do fornecedor",
        ///         "cnjpFornecedor" : "94635104000116"
        ///     }
        /// 
        /// </param>
        /// <returns>Um produto em específico</returns>
        /// <response code="201">Retorna 201(CREATED) caso o cadastro seja efetuado com sucesso.</response>
        /// <response code="400">Retorna 400(BAD REQUEST) caso ocorra algum erro durante o cadastro.</response>
        [HttpPost]
        public IActionResult Post(ProdutoRequestModel request)
        {
            try
            {
                var response = _produtoService.Post(request);

                return Created($"api/produtos/{response.ToString()}", null);

                return Created("", null);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });

            }
        }
    }
}
