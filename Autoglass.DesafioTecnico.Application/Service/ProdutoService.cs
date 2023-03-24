using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Domain.Model;
using Autoglass.DesafioTecnico.Infrastructure.Repository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Autoglass.DesafioTecnico.Application.Service
{
    public class ProdutoService
    {
        private ProdutoRepository _produtoRepository;
        private IMapper _produtoMapper;
        private int _defaultPageRecordCount = 10;

        public ProdutoService(ProdutoRepository produtoRepository, IMapper produtoMapper)
        {
            _produtoRepository = produtoRepository;
            _produtoMapper = produtoMapper;
        }

        public virtual void Delete(int id) =>
            _produtoRepository.Delete(id);

        public virtual GetAllProdutoResponseModel GetAll(GetAllProdutoRequestModel request)
        {
            var takePage = request.Page ?? 1;
            var takeCount = request.CountPerPage ?? _defaultPageRecordCount;

            var totalFromDb = _produtoRepository.GetAll().Where(x =>
                (x.CodigoFornecedor == request.CodigoFornecedor || request.CodigoFornecedor == 0) &&
                (x.CNPJFornecedor == request.CNPJFornecedor || request.CNPJFornecedor == null)
            );

            var response = new GetAllProdutoResponseModel(takePage, totalFromDb.Count());

            var paginated = totalFromDb.Skip((takePage - 1) * takeCount)
                .Take(takeCount)
                .ToList();

            foreach (var item in paginated)
            {
                response.Data.Add(_produtoMapper.Map<ProdutoResponseModel>(item));
            }

            return response;
        }

        public virtual ProdutoResponseModel GetById(int codigo)
        {
            var fromDb = _produtoRepository.GetById(codigo);

            var response = _produtoMapper.Map<ProdutoResponseModel>(fromDb);

            return response;
        }

        public virtual void Patch(int id, JsonPatchDocument<Produto> produto)
        {
            var fromDb = _produtoRepository.GetById(id);

            if (fromDb != null)
            {
                produto.ApplyTo(fromDb);

                _produtoRepository.Patch(fromDb);
            }


        }

        public virtual int Post(ProdutoRequestModel request)
        {
            if (request.DataValidade <= request.DataFabricacao)
                throw new ArgumentException("A Data de Validade não pode ser menor que a de Data de Fabricação!");

            if (string.IsNullOrWhiteSpace(request.Descricao))
                throw new ArgumentException("O Campo Descrição deve ser preenchido!");

            request.CNPJFornecedor = Regex.Replace(request.CNPJFornecedor, "[^0-9]+", "");

            return _produtoRepository.Post(_produtoMapper.Map<Produto>(request));

        }
    }
}
