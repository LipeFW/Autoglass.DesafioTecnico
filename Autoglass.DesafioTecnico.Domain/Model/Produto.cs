using System;
using System.Text.RegularExpressions;

namespace Autoglass.DesafioTecnico.Domain.Model
{
    public class Produto : BaseEntity
    {
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
            CNPJFornecedor = Regex.Replace(cnpjFornecedor, "[^0-9]+", "");
            Situacao = true;
        }

        public Produto()
        {

        }
    }
}
