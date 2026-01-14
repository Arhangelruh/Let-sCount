namespace LetusCountService.Application.Interfaces
{
	public interface IWorkerService
	{
		/// <summary>
		/// Service for parsing files and save data to base.
		/// </summary>
		/// <param name="filePath">Path to file</param>
		/// <param name="ct">Cancelattion token</param>
		Task WorkAsync(string filePath, CancellationToken ct);
	}
}
