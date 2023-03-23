using System;

namespace Autoglass.DesafioTecnico.Domain.Domain
{
    public class Produto
    {
        public int Codigo { get; set; }
        public bool Situacao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string? DescricaoFornecedor { get; set; }
        public string? CNPJFornecedor { get; set; }

        public Produto(string descricao, DateTime dataFabricacao, DateTime dataValidade, int codigoFornecedor,
            string descricaoFornecedor, string cnpjFornecedor)
        {
            Descricao = descricao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CNPJFornecedor = cnpjFornecedor;
            Situacao = true;
        }

        public Produto()
        {

        }
    }
}
