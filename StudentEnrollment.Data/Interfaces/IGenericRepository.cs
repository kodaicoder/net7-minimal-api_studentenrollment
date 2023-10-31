namespace StudentEnrollment.Data.Interfaces
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		Task<List<TEntity>> GetAllAsync();
		Task<TEntity> GetByIdAsync(int id);
		Task<TEntity> CreateAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task<bool> DeleteAsync(int id);
		Task<bool> IsExistAsync(int id);
	}
}
