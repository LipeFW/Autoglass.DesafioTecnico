using Autoglass.DesafioTecnico.Application.Dto;
using Autoglass.DesafioTecnico.Domain.Model;
using Autoglass.DesafioTecnico.Infrastructure.Repository;
using AutoMapper;
using System;

namespace Autoglass.DesafioTecnico.Application.Service
{
    public class ProdutoService
    {
        private ProdutoRepository _produtoRepository;
        private IMapper _produtoMapper;

        public ProdutoService(ProdutoRepository produtoRepository, IMapper produtoMapper)
        {
            _produtoRepository = produtoRepository;
            _produtoMapper = produtoMapper;
        }

        public virtual int Post(ProdutoRequestModel request)
        {
            if (request.DataValidade <= request.DataFabricacao)
                throw new ArgumentException("A Data de Validade não pode ser menor que a de Data de Fabricação!");

            return _produtoRepository.Post(_produtoMapper.Map<Produto>(request));

        }
    }
}
