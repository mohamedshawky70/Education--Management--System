using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
