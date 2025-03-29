namespace School.Infrastructure.EntitiesConfiguration
{
	class DepartmentConfig : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(x => x.Name).HasMaxLength(100);
		}
	}
}
