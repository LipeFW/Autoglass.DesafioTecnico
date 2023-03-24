using System.Collections.Generic;

namespace Autoglass.DesafioTecnico.Application.Dto
{
    public class GetAllProdutoResponseModel
    {
        public int? Page { get; set; }
        public int? TotalItems { get; set; }
        public List<ProdutoResponseModel> Data { get; set; } = new List<ProdutoResponseModel>();

        public GetAllProdutoResponseModel(int? page, int? total)
        {
            Page = page;
            TotalItems = total;
        }
    }
}
