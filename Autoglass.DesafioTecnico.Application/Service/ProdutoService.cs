using Autoglass.DesafioTecnico.Infrastructure.Repository;

namespace Autoglass.DesafioTecnico.Application.Service
{
    public class ProdutoService
    {
        private ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
    }
}
