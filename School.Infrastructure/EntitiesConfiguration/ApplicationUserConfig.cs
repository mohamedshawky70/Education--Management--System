namespace School.Infrastructure.EntitiesConfiguration;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{

		builder.Property(x => x.FirstName)
			.HasMaxLength(100);
		builder.Property(x => x.LastName)
			.HasMaxLength(100);
	}
}
