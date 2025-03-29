namespace School.Infrastructure.EntitiesConfiguration
{
	class SubjectConfig : IEntityTypeConfiguration<Subject>
	{
		public void Configure(EntityTypeBuilder<Subject> builder)
		{
			builder.Property(x => x.Name).HasMaxLength(100);
		}
	}
}
