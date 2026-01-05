using System.Xml.Serialization;

public class HeadercardUnitXml
{
	[XmlAttribute] public string StartTime { get; set; }
	[XmlAttribute] public string EndTime { get; set; }
	[XmlAttribute] public string Currency { get; set; }
	[XmlAttribute] public string Rejects { get; set; }

	[XmlElement("Counter")]
	public List<CounterXml> Counters { get; set; } = [];

	[XmlElement("Sum")]
	public SumXml Sum { get; set; }

	[XmlElement("SerialNumber")]
	public List<SerialNumberXml> SerialNumbers { get; set; } = [];
}
