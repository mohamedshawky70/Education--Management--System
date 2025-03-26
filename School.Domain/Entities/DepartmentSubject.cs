using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
	public class DepartmentSubject
    {
		public int Id { get; set; }
		public int DepartmentId { get; set; }
		public int SubjectId { get; set; }
		public virtual Department? Department { get; set; }
		public virtual Subject? Subjects { get; set; }
	}
}
