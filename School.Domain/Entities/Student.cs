using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
    public class Student
    {
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Address { get; set; } = default!;
		public string Phone { get; set; } = default!;
		public int? DepartmentId { get; set; }
		public virtual Department Department { get; set; } = default!;
		public virtual ICollection<Assignment> Assignment { get; set; } = new HashSet<Assignment>();
	}
}
