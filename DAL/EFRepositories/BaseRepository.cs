using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstraction;
using Entities;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace DAL.EFRepositories
{
    public class BaseRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TKey : IComparable where TEntity : BaseEntity<TKey>
    {
        public BaseRepository(DbContext context)
        {
            Context = context;
            Items = Context.Set<TEntity>();
        }

        private DbSet<TEntity> Items { get; set; }
        protected DbContext Context { get; }

        public virtual List<TEntity> GetAll()
        {
            return Items.ToList();
        }

        public virtual TEntity GetById(TKey id)
        {
            return Items.Find(id);
        }

        public virtual TEntity GetBy(Func<TEntity, bool> predicate)
        {
            return Items.First(predicate);
        }

        public virtual List<TEntity> GetMany(Func<TEntity, bool> pridicate)
        {
            return Items.Where(pridicate).ToList();
        }

        public virtual void Remove(TEntity entity)
        {
            Items.Remove(Items.Find(entity.Id));
            SaveChanges();
        }

        public virtual void Add(TEntity entity)
        {
            Items.Add(entity);
            SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            Context.Database.ExecuteSqlCommand(
                "insert or replace into " + typeof(TEntity).Name + "s (Id, Name) values ('" + entity.Id + "', '" + entity.Name + "');"
            );
            SaveChanges();
        }

        public virtual List<TEntity> GetRange(TEntity startEntity, int count)
        {
            var startPosition = Enumerable.TakeWhile(Items, item => startEntity.Id.CompareTo(item.Id) != 0).Count();
            return Items.OrderBy(i => i.Id).Skip(startPosition).Take(count).ToList();
        }

        public virtual List<TEntity> GetRange(int toSkip, int count)
        {
            return Items.OrderBy(i => i.Id).Skip(toSkip).Take(count).ToList();
        }

        public void AddRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.Add(entity);
            }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
