using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Entities
{
	public class Department
    {
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public virtual ICollection<Student> Students { get; set; }=new HashSet<Student>();
		public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }= new HashSet<DepartmentSubject>();
	}
}
