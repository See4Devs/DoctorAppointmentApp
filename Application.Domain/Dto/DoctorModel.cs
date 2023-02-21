using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Domain.Dao;

namespace Application.Domain.Dto
{
    public class DoctorModel
	{
		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }

		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Specialty { get; set; }

		public Doctor TransformToDto()
		{
			Doctor newObject = new Doctor();
			newObject.Name = this.Name;
			newObject.Specialty = this.Specialty;
			return newObject;
		}
	}
}

