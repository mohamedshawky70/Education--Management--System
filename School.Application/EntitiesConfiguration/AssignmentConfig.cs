namespace School.Application.EntitiesConfiguration
{
	class AssignmentConfig : IEntityTypeConfiguration<Assignment>
	{
		public void Configure(EntityTypeBuilder<Assignment> builder)
		{
			builder.Property(x => x.Title).HasMaxLength(100);
		}
	}
}
