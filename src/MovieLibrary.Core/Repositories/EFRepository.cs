using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MovieLibrary.Api.Common;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Data.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly MovieLibraryContext _context;
        private readonly DbSet<TEntity> _table;
        public EFRepository(MovieLibraryContext context = null)
        {
            _context = context ?? new MovieLibraryContext();
            _table = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll() => _table.ToList();

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> config) 
            => config.Invoke(_context.Set<TEntity>()).ToList();

        public TEntity GetById(int id) => _table.Find(id);
        public int Insert(TEntity entity)
        {
            var addedEntity = _table.Add(entity);
            _context.SaveChanges();
            return addedEntity.Entity.Id;
        }
        public void Update(TEntity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            TEntity existing = _table.Find(id);
            _table.Remove(existing);
            _context.SaveChanges();
        }
    }
}
