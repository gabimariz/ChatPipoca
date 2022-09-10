using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class UserMap
{
	public UserMap(EntityTypeBuilder<User> entityTypeBuilder)
	{
		entityTypeBuilder.HasKey(p => p.Id);

		entityTypeBuilder.Property(p => p.Email)
			.IsRequired()
			.HasMaxLength(60);

		entityTypeBuilder.HasIndex(p => p.Email)
			.IsUnique();

		entityTypeBuilder.Property(p => p.Password)
			.IsRequired();
	}
}
