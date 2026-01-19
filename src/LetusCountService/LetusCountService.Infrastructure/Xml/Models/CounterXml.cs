using System.Xml.Serialization;

namespace LetusCountService.Infrastructure.Xml.Models
{
	public class CounterXml
	{
		[XmlAttribute] public int Number { get; set; }
		[XmlAttribute] public string Currency { get; set; }
		[XmlAttribute] public string DenomName { get; set; }
		[XmlAttribute] public int Value { get; set; }
		[XmlAttribute] public string Issue { get; set; }
		[XmlAttribute] public string Quality { get; set; }
		[XmlAttribute] public string Output { get; set; }
	}
}