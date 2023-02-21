using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Domain.Dao;

namespace Application.Domain.Dto
{
	public class PatientModel
	{
		[Required]
		[MaxLength(150)]
		[MinLength(5)]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[MaxLength(500)]
		public string Address { get; set; }

		public DateTime DateOfBirth { get; set; }

		public Patient TransformToDto()
		{
			Patient newObject = new Patient();
			newObject.Name = this.Name;
			newObject.Email = this.Email;
			newObject.Address = this.Address;
			newObject.DateOfBirth = this.DateOfBirth;
			return newObject;
		}
	}
}

