using InternalPortal.Core.Interfaces;
using LetusCountService.Application.Interfaces;
using LetusCountService.Domain.Models;

namespace LetusCountService.Application.Services.EFServices
{
	/// <inheritdoc/>
	public class BanknotesService(IRepository<Banknote> repository) : IBanknotesService
	{
		private readonly IRepository<Banknote> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

		public async Task AddBanknoteAsync(Banknote banknote)
		{
			ArgumentNullException.ThrowIfNull(banknote);

			await _repository.AddAsync(banknote);
			await _repository.SaveChangesAsync();
		}
	}
}
