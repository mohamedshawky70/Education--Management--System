namespace School.Application.EntitiesConfiguration
{
	class TeacherConfig : IEntityTypeConfiguration<Teacher>
	{
		public void Configure(EntityTypeBuilder<Teacher> builder)
		{
			builder.Property(x => x.Name).HasMaxLength(100);
			builder.Property(x => x.Email).HasMaxLength(500);
		}
	}
}
