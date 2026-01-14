using InternalPortal.Core.Interfaces;
using LetusCountService.Application.Interfaces;
using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Services.EFServices
{
	public class OperationUnitService(IRepository<OperationUnit> repository) : IOperationUnitService
	{
		private readonly IRepository<OperationUnit> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
		public async Task<int> AddOperationUnitAsync(OperationUnit operationUnit)
		{
			ArgumentNullException.ThrowIfNull(operationUnit);

			await _repository.AddAsync(operationUnit);
			await _repository.SaveChangesAsync();

			return operationUnit.Id;
		}
	}
}
