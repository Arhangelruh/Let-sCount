using System.Xml.Serialization;

public class SerialNumberXml
{
	[XmlAttribute] public string DenomName { get; set; }
	[XmlAttribute("SN")] public string Serial { get; set; }
}
