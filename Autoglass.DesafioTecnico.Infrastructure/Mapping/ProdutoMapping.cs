using Autoglass.DesafioTecnico.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoglass.DesafioTecnico.Infrastructure.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Descricao).IsRequired();
            builder.Property(p => p.Situacao).IsRequired();
            builder.Property(p => p.DataFabricacao);
            builder.Property(p => p.DataValidade);
            builder.Property(p => p.CodigoFornecedor);
            builder.Property(p => p.DescricaoFornecedor);
            builder.Property(p => p.CNPJFornecedor);

            builder.ToTable("Produtos");
        }
    }
}
