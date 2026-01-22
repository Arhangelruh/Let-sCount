namespace LetusCountApplication.Application.Exceptions
{
	/// <summary>
	/// Exception writing data to database.
	/// </summary>
	public sealed class PersistenceException : Exception
	{
		public PersistenceException(
			string message,
			Exception innerException)
			: base(message, innerException)
		{
		}

		public PersistenceException(Exception innerException)
			: base("Error, can't save data to database.", innerException)
		{
		}
	}
}
