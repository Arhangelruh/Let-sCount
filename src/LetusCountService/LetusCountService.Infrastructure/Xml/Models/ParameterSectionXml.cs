using System.Xml.Serialization;

public class ParameterSectionXml
{
	[XmlAttribute] public string OpModeName { get; set; }
	[XmlAttribute] public string FitnessMode { get; set; }
	[XmlAttribute] public string ExtraMode { get; set; }
	[XmlAttribute] public string StartTime { get; set; }
	[XmlAttribute] public string EndTime { get; set; }

	[XmlElement("Operator")]
	public string Operator { get; set; }

	[XmlElement("HeadercardUnit")]
	public List<HeadercardUnitXml> HeadercardUnits { get; set; } = [];
}
