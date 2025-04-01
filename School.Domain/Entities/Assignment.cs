namespace School.Domain.Entities
{
	public class Assignment
	{
		public int Id { get; set; }
		public string Title { get; set; } = default!;
		public DateTime DueDate { get; set; }
		public byte MaxScore { get; set; }
		public int SubjectId { get; set; }
		public Subject? Subject { get; set; }
		public int StudentId { get; set; }
		public Student? Student { get; set; }
	}
}
