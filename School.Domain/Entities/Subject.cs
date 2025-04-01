namespace School.Domain.Entities
{
	public class Subject
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public DateTime Period { get; set; }
		public int TeacherId { get; set; }
		public Teacher? Teacher { get; set; }
		public virtual ICollection<StudentSubject> StudentsSubjects { get; set; } = new HashSet<StudentSubject>();//Subject not allow duplicated in the same student
		public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = new HashSet<DepartmentSubject>();//Subject not allow duplicated in the same department
		public virtual ICollection<Assignment> Assignment { get; set; } = new HashSet<Assignment>();
	}
}
