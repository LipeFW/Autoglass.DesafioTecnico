using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Domain.Model;
using Autoglass.DesafioTecnico.Infrastructure.Repository;
using AutoMapper;
using System;
using System.Linq;

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

        public virtual bool Delete(int id) =>
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

        public virtual int Post(ProdutoRequestModel request)
        {
            if (request.DataValidade <= request.DataFabricacao)
                throw new ArgumentException("A Data de Validade não pode ser menor que a de Data de Fabricação!");

            return _produtoRepository.Post(_produtoMapper.Map<Produto>(request));

        }
    }
}
