using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.Abstraction
{
    public interface IRepository<TKey, TEntity> where TKey : IComparable where TEntity : BaseEntity<TKey>
    {
        List<TEntity> GetAll();
        TEntity GetById(TKey id);
        TEntity GetBy(Func<TEntity, bool> pridicate);
        List<TEntity> GetMany(Func<TEntity, bool> pridicate);
        void Remove(TEntity entity);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddRange(List<TEntity> entities);
        List<TEntity> GetRange(TEntity stertEntity, int count);
        List<TEntity> GetRange(int toSkip, int count);
    }
}
