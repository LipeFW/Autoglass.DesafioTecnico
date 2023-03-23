using Autoglass.DesafioTecnico.Domain.Model;
using Autoglass.DesafioTecnico.Infrastructure.Context;

namespace Autoglass.DesafioTecnico.Infrastructure.Repository
{
    public class ProdutoRepository : Repository<Produto>
    {
        public ProdutoRepository(MainContext context) : base(context)
        {
        }
    }
}
