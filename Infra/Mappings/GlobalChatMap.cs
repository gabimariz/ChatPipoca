using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class GlobalChatMap
{
	public GlobalChatMap(EntityTypeBuilder<GlobalChat> entityTypeBuilder)
	{
		entityTypeBuilder.HasKey(p => p.Id);

		entityTypeBuilder.Property(p => p.Message)
			.IsRequired()
			.HasMaxLength(160);

		entityTypeBuilder.HasOne(p => p.Profile)
			.WithOne(p => p.GlobalChat)
			.HasForeignKey<GlobalChat>(p => p.From)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
