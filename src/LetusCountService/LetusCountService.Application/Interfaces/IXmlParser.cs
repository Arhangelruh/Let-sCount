namespace LetusCountService.Application.Interfaces
{
	/// <summary>
	/// Xml parser.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IXmlParser<T>
	{
		/// <summary>
		/// Parse.
		/// </summary>
		/// <param name="stream">file stream</param>
		/// <param name="ct">concellation token</param>
		/// <returns>domain model</returns>
		Task<T> ParseAsync(Stream stream, CancellationToken ct);
	}
}
