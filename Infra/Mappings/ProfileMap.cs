using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class ProfileMap
{
	public ProfileMap(EntityTypeBuilder<Profile> entityTypeBuilder)
	{
		entityTypeBuilder.HasKey(p => p.Id);

		entityTypeBuilder.Property(p => p.Firstname)
			.IsRequired()
			.HasMaxLength(50);

		entityTypeBuilder.Property(p => p.Lastname)
			.IsRequired()
			.HasMaxLength(120);

		entityTypeBuilder.Property(p => p.PhoneNumber)
			.HasMaxLength(15);
		entityTypeBuilder.HasIndex(p => p.PhoneNumber)
			.IsUnique();

		entityTypeBuilder.HasOne(p => p.User)
			.WithOne(p => p.Profile)
			.HasForeignKey<Profile>(p => p.UserId)
			.OnDelete(DeleteBehavior.Cascade)
			.IsRequired();
	}
}
