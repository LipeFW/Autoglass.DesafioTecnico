using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Application.Service;
using Autoglass.DesafioTecnico.Domain.Model;
using Microsoft.AspNetCore.JsonPatch;
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

                return Created($"api/produtos/{response}", null);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });

            }
        }

        /// <summary>
        /// Lista todos os produtos cadastrados.
        /// </summary>
        /// <returns>Os produtos cadastrados</returns>
        /// <response code="200">Retorna os produtos cadastrados na base de dados.</response>
        [HttpGet]
        public IActionResult GetAll([FromQuery] GetAllProdutoRequestModel request)
        {
            var response = _produtoService.GetAll(request);

            return Ok(response);
        }

        /// <summary>
        /// Busca um produto com um código específico.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET api/Produtos/1
        ///
        /// </remarks>
        /// <param name="id">   O Código do produto a ser pesquisado</param>
        /// <returns>Um produto em específico</returns>
        /// <response code="200">Retorna OK caso exista um produto com o código informado.</response>
        /// <response code="404">Retorna NOT FOUND caso não exista nenhum produto com o código informado.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _produtoService.GetById(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Desativa o cadastro de um produto específico.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     DELETE api/produtos/1
        ///
        /// </remarks>
        /// <param name="id">   Id do produto a ser desativado.</param>
        /// <response code="204">Retorna NO CONTENT.</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _produtoService.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Atualiza as informações de um produto específico.
        /// </summary>
        /// <remarks>
        ///
        /// </remarks>
        /// <param name="id">   Id do produto a ser atualizado.</param>
        /// <param name="patchDoc">   Corpo com as informações do produto a serem atualizadas.
        /// Exemplo:
        ///  
        ///PATCH api/produtos/1
        /// 
        ///     [
        ///         {
        ///             "path": "/descricao",
        ///             "op": "replace",
        ///             "value": "Nova descricao do produto"
        ///         }
        ///     ]
        ///
        /// </param>
        /// <response code="200">Retorna OK se as informações forem atualizadas com sucesso.</response>
        /// <response code="400">Retorna BAD REQUEST se houver algum problma durante a atualização dos dados.</response>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Produto> patchDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _produtoService.Patch(id, patchDoc);

            return Ok();
        }
    }
}
