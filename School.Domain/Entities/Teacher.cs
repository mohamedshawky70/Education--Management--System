using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
	public class Teacher
    {
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Email { get; set; } = default!;
		public int SubjectId { get; set; } = default!;
		public ICollection<Subject> Subject { get; set; } = new HashSet<Subject>(); 
	}
}
