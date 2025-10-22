﻿using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts {
    public interface IUnitOfWork {
    
        IGenericRepository<TKey, TEntity> GetRepository<TKey,TEntity>() where TEntity : BaseEntity<TKey>;

        Task<int> SaveChangesAsync();

    }
}
