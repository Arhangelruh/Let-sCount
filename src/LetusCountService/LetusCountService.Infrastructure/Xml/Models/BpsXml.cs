using System.Xml.Serialization;

[XmlRoot("BPS")]
public class BpsXml
{
	[XmlAttribute("created")]
	public string Created { get; set; }

	[XmlAttribute("version")]
	public string Version { get; set; }

	[XmlElement("Machine")]
	public MachineXml Machine { get; set; }
}
