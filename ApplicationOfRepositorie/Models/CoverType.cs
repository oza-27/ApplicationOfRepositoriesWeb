using System.ComponentModel.DataAnnotations;

namespace ApplicationOfRepositorie.Models
{
	public class CoverType
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
