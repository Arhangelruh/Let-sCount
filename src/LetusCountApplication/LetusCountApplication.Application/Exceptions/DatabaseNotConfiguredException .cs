namespace LetusCountApplication.Application.Exceptions
{
	public sealed class DatabaseNotConfiguredException : ApplicationException
	{
		public DatabaseNotConfiguredException(string message, Exception innerException)
			: base(message, innerException) { }

		public DatabaseNotConfiguredException(Exception innerException)
			: base("Error, database connection not configured.", innerException)
		{
		}
	}
}
