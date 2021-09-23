using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Repo.RepositoryUsingEFinMVC.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _2._NTBrokersDataBase.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private RealEstateEfCoreContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(RealEstateEfCoreContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public void Insert(T obj)
        {
            _dbSet.Add(obj);
        }
        public void Update(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = _dbSet.Find(id);
            _dbSet.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
