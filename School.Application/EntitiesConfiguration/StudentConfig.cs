namespace School.Application.EntitiesConfiguration
{
	class StudentConfig : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.Property(x => x.Name).HasMaxLength(100);
			builder.Property(x => x.Address).HasMaxLength(500);
			builder.Property(x => x.Phone).HasMaxLength(11);
		}
	}
}
