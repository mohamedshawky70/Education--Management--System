namespace School.Application.EntitiesConfiguration
{
	class DepartmentConfig : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(x => x.DepartmentName).HasMaxLength(100);
		}
	}
}
