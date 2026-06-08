using System.Xml.Serialization;

namespace LetusCountService.Infrastructure.Xml.Models
{
	public class SerialNumberXml
	{
		[XmlAttribute] public string DenomName { get; set; }
		[XmlAttribute("SN")] public string Serial { get; set; }
	}
}