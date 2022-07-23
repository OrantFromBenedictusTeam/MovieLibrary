using Microsoft.EntityFrameworkCore.Query;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Api.Common
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        int Insert(TEntity entity);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> config);
        TEntity GetById(int id);
        void Update(TEntity entity);
        void Delete(int id);
    }
}