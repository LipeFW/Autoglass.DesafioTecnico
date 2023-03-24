namespace Autoglass.DesafioTecnico.Application.Dto
{
    public class GetAllProdutoRequestModel
    {
        public int CodigoFornecedor { get; set; }
        public string CNPJFornecedor { get; set; }
        public int? Page { get; set; }
        public int? CountPerPage { get; set; }
    }
}
