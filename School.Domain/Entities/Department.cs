namespace School.Domain.Entities
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
		public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = new HashSet<DepartmentSubject>();
	}
}
