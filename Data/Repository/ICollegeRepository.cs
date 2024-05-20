using System.Linq.Expressions;

namespace CollegeProject.Data.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Expression<Func<T, bool>> filter, bool useNoTracking = false);
        Task<T> GetByName(Expression<Func<T, bool>> filter);
        Task<T> Create(T dbRecord);
        Task<T> Update(T dbRecord);
        Task<bool> Delete(T dbRecord);
    }
}
