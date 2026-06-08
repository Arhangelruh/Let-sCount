using LetusCountService.Application.Interfaces;
using LetusCountService.Domain.Models;
using LetusCountService.Infrastructure.Xml.Models;
using System.Xml.Serialization;

namespace LetusCountService.Infrastructure.Xml.Services
{
	public class XmlParser() : IXmlParser<Operation>
	{

		public Task<Operation> ParseAsync(Stream xmlStream, CancellationToken ct)
		{
			var serializer = new XmlSerializer(typeof(BpsXml));
			var model = (BpsXml)serializer.Deserialize(xmlStream)!;
			return Task.FromResult(XmlMapper.ToDomain(model));
		}

		public static async Task<T> DeserializeXmlAsync<T>(Stream xmlStream, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(xmlStream);

			var serializer = new XmlSerializer(typeof(T));

			return await Task.Run(() =>
			{
				cancellationToken.ThrowIfCancellationRequested();
				return (T)serializer.Deserialize(xmlStream)!;
			}, cancellationToken);
		}
	}
}
