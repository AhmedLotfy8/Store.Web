using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts {
    public interface IGenericRepository<Tkey, TEntity> where TEntity : BaseEntity<Tkey> {


        Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false);

        Task<TEntity?> GetAsync(Tkey key);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


    }
}
