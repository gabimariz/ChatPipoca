using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class TokenMap
{
	public TokenMap(EntityTypeBuilder<Token> entityTypeBuilder)
	{
		entityTypeBuilder.HasKey(p => p.Id);

		entityTypeBuilder.Property(p => p.Pass)
			.IsRequired();

		entityTypeBuilder.Property(p => p.UserId)
			.IsRequired();
	}
}
