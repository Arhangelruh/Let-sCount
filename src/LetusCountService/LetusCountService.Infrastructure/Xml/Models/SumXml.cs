using System.Xml.Serialization;

public class SumXml
{
	[XmlAttribute] public int Number { get; set; }
	[XmlAttribute] public string Currency { get; set; }
	[XmlAttribute] public string Output { get; set; }
	[XmlAttribute] public decimal TotalSum { get; set; }
	[XmlAttribute] public decimal Coins { get; set; }
	[XmlAttribute] public decimal Sum { get; set; }
}
