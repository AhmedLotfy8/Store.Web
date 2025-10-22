﻿using Store.Domain.Contracts;
using Store.Domain.Entities;
using Store.Persistence.Data.Contexts;
using Store.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence {
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork {

        private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();

        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey> {
            return (IGenericRepository<TKey, TEntity>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TKey, TEntity>(_context));
        }

        #region Same logic using Dictionary

        //private Dictionary<string, object> _repositories = new Dictionary<string, object>();

        //public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey> {

        //    var type = typeof(TEntity).Name;

        //    if (!_repositories.ContainsKey(type)) {

        //        var repository = new GenericRepository<TKey, TEntity>(_context);
        //        _repositories.Add(type, repository);

        //    }

        //    return (IGenericRepository<TKey, TEntity>) _repositories[type];

        //}

        #endregion



        public async Task<int> SaveChangesAsync() {
            return await _context.SaveChangesAsync();
        }

    }
}
