using LetusCountApplication.Domain.Constants;
using LetusCountApplication.Domain.Models;
using LetusCountApplication.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetusCountApplication.Infrastructure.Data.Configurations
{
	public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
	{
		public void Configure(EntityTypeBuilder<Profile> builder)
		{
			builder = builder ?? throw new ArgumentNullException(nameof(builder));

			builder.ToTable(TableConstants.Profiles, SchemaConstants.Users)
				.HasKey(profile => profile.Id);

			builder.Property(profile => profile.UserId)
				.IsRequired();

			builder.Property(profile => profile.FirstName)
				.HasMaxLength(FieldLengthsConstants.MaxLengthShortMedium);

			builder.Property(profile => profile.LastName)
				.HasMaxLength(FieldLengthsConstants.MaxLengthShortMedium);

			builder.Property(profile => profile.MiddleName)
				.HasMaxLength(FieldLengthsConstants.MaxLengthShortMedium);

			builder.HasOne(profile => profile.User)
				.WithOne(user => user.Profile)
				.HasForeignKey<Profile>(profile => profile.UserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
