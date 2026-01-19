using LetusCountService.Domain.Models;
using LetusCountService.Infrastructure.Xml.Models;

namespace LetusCountService.Infrastructure.Xml.Services
{
	public static class XmlMapper
	{
		public static Operation ToDomain(BpsXml xml)
		{
			ArgumentNullException.ThrowIfNull(xml);

			var machineSerial = xml.Machine.SerialNumber;

			var operationUnits = new List<OperationUnit>();
			foreach (var unit in xml.Machine.ParameterSection.HeadercardUnits)
			{

				var banknotes = new List<Banknote>();
				foreach (var banknote in unit.SerialNumbers)
				{
					var dataString = banknote.DenomName.Split(" ");
					var denomName = dataString[1];
					var serialNumber = banknote.Serial;
					var value = int.Parse(dataString[0]);

					banknotes.Add(new Banknote
					{
						DenomName = denomName,
						Value = value,
						SerialNumber = serialNumber
					});
				}
				operationUnits.Add(new OperationUnit
				{
					Currency = unit.Currency,
					Banknotes = banknotes
				});
			}

			return new Operation
			{
				MachineSerial = machineSerial,
				OperationUnits = operationUnits
			};

		}
	}
}
