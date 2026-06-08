using LetusCountService.Infrastructure.Xml.Models;
using System.Xml.Serialization;

public class MachineXml
{
	[XmlAttribute] public string Type { get; set; }
	[XmlAttribute] public string Variant { get; set; }
	[XmlAttribute] public string InstallationDate { get; set; }
	[XmlAttribute] public string Name { get; set; }
	[XmlAttribute] public string VersionInfo { get; set; }
	[XmlAttribute] public string SoftwareRelease { get; set; }
	[XmlAttribute] public string Site { get; set; }
	[XmlAttribute] public string SerialNumber { get; set; }

	[XmlElement("ParameterSection")]
	public ParameterSectionXml ParameterSection { get; set; }
}
