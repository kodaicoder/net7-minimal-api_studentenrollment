using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Interfaces;

namespace StudentEnrollment.Data.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		protected readonly StudentEnrollmentDbContext _db;

		public GenericRepository(StudentEnrollmentDbContext db)
		{
			this._db = db;
		}


		public async Task<TEntity> CreateAsync(TEntity entity)
		{
			await _db.AddAsync(entity);
			await _db.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			TEntity record = await GetByIdAsync(id);
			_db.Set<TEntity>().Remove(record);
			//? because SaveChangesAsync() returns int of records affected by the operation
			return await _db.SaveChangesAsync() > 0;
		}

		public async Task<List<TEntity>> GetAllAsync()
		{
			return await _db.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity> GetByIdAsync(int id)
		{
			return await _db.Set<TEntity>().FindAsync(id);
		}

		public async Task<bool> IsExistAsync(int id)
		{
			return await _db.Set<TEntity>().AnyAsync(q => q.Id == id);
		}

		public async Task UpdateAsync(TEntity entity)
		{
			_db.Set<TEntity>().Update(entity);
			await _db.SaveChangesAsync();
		}
	}
}
