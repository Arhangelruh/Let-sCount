using InternalPortal.Core.Interfaces;
using LetusCountApplication.Application.Exceptions;
using LetusCountApplication.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InternalPortal.Infrastucture.Data.Repository
{
	/// <inheritdoc cref="IRepository<T>"/>
	public class Repository<T>(LetusCountApplicationContext context) : IRepository<T> where T : class
	{
		private readonly DbSet<T> _dbSet = context.Set<T>();
		private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await _dbSet.AddRangeAsync(entities);
		}

		public void Delete(T entity)
		{
			_dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entity)
		{
			_dbSet.RemoveRange(entity);
		}

		public IQueryable<T> GetAll()
		{
			return _dbSet.AsNoTracking();
		}

		public async Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate);
		}

		public async Task<T> GetEntityWithoutTrackingAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
		}

		public async Task SaveChangesAsync()
		{
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new PersistenceException(ex);
			}
			catch (InvalidOperationException ex)
			when (ex.InnerException.Message.Contains("Failed to connect"))
			{
				throw new DatabaseNotConfiguredException(ex);
			}
		}

		public void Update(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}