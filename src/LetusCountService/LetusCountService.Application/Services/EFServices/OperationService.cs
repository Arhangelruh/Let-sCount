using InternalPortal.Core.Interfaces;
using LetusCountService.Application.Interfaces;
using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Services.EFServices
{
	public class OperationService(IRepository<Operation> repository) : IOperationService
	{
		private readonly IRepository<Operation> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

		public async Task<int> AddOperationAsync(Operation operation)
		{
			ArgumentNullException.ThrowIfNull(operation);

			await _repository.AddAsync(operation);
			await _repository.SaveChangesAsync();

			return operation.Id;
		}
	}
}
