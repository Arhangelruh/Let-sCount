using LetusCountApplication.Domain.Models;

namespace LetusCountApplication.Application.DTOModels
{
	public class FullOperationInformationDto
	{
		/// <summary>
		/// Department information.
		/// </summary>
		public DepartmentDto Department { get; set; }

		/// <summary>
		/// Operation information.
		/// </summary>
		public OperationDto Operation { get; set; }
	}
}
