namespace School.Domain.Entities
{
	public class StudentSubject
	{
		public int Id { get; set; }
		public int StudentId { get; set; }
		public int SubjectId { get; set; }
		public virtual Student? Student { get; set; }
		public virtual Subject? Subject { get; set; }
	}
}
