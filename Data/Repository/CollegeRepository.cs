using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollegeProject.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _collegeDBContext;
        private DbSet<T> _dbSet;
        public CollegeRepository(CollegeDBContext collegeDBContext)
        {
            _collegeDBContext = collegeDBContext;
            _dbSet = _collegeDBContext.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> Get(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            }
            else
            {
                return await _dbSet.Where(filter).FirstOrDefaultAsync();
            }
        }
        //as we are already using the same above
        //public async Task<T> GetByName(Expression<Func<T, bool>> filter)
        //{
        //    return await _dbSet.Where(filter).FirstOrDefaultAsync();
        //}
        public async Task<T> Create(T dbRecord)
        {
            await _dbSet.AddAsync(dbRecord);
            await _collegeDBContext.SaveChangesAsync();
            return dbRecord;
        }
        public async Task<T> Update(T dbRecord)
        {
            _collegeDBContext.Update(dbRecord);
            await _collegeDBContext.SaveChangesAsync();
            return dbRecord;
        }
        public async Task<bool> Delete(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _collegeDBContext.SaveChangesAsync();
            return true;
        }
    }
}
