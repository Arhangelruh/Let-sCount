using LetusCountService.Application.Interfaces;
using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Services
{
	/// <inheritdoc/>
	public class SaveOperationService(
		IRepository<Banknote> repository,
		IRepository<OperationUnit> repositoryOperationUnit,
		IRepository<Operation> repositoryOperation
		) : ISaveOperationService
	{
		private readonly IRepository<Banknote> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
		private readonly IRepository<OperationUnit> _repositoryOperationUnit = repositoryOperationUnit ?? throw new ArgumentNullException(nameof(repository));
		private readonly IRepository<Operation> _repositoryOperation = repositoryOperation ?? throw new ArgumentNullException(nameof(repository));
		public async Task SaveOperationAsync(Operation operation)
		{
			ArgumentNullException.ThrowIfNull(operation);

			var newOperation = new Operation
			{
				MachineSerial = operation.MachineSerial,
				StartTime = operation.StartTime.ToUniversalTime(),
				EndTime = operation.EndTime.ToUniversalTime(),
			};

			await _repositoryOperation.AddAsync(newOperation);
			await _repositoryOperation.SaveChangesAsync();

			foreach (var unit in operation.OperationUnits)
			{
				var operationUnit = new OperationUnit
				{
					OperationId = newOperation.Id,
					Currency = unit.Currency
				};

				await _repositoryOperationUnit.AddAsync(operationUnit);
				await _repositoryOperationUnit.SaveChangesAsync();

				foreach (var banknote in unit.Banknotes)
				{
					var newBanknote = new Banknote
					{
						OperationUnitId = operationUnit.Id,
						DenomName = banknote.DenomName,
						SerialNumber = banknote.SerialNumber,
						Value = banknote.Value
					};

					await _repository.AddAsync(newBanknote);
					await _repository.SaveChangesAsync();
				}
			}
		}
	}
}
