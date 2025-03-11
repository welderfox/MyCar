using Microsoft.EntityFrameworkCore;
using MyCar.Common.Model;

namespace MyCar.Common.BaseRepository.Interface
{
    public interface IBaseRepository<TModel, TContext>
        where TModel : BaseModel
        where TContext : DbContext
    {

        #region Gets
        Task<List<TModel>> GetAsync(CancellationToken cancellationToken);
        Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TModel>> GetAsync(List<Guid> ids, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken);

        #endregion

        #region Insert

        Task<TModel> InsertAsync(TModel model, CancellationToken cancellationToken);
        Task InsertAsync(List<TModel> models, CancellationToken cancellationToken);

        #endregion

        #region Update

        Task UpdatetAsync(List<TModel> models, CancellationToken cancellationToken);

        #endregion

        #region Metodos Delete

        Task DeletAsync(Guid id, CancellationToken cancellationToken);
        Task DeletAsync(TModel model, CancellationToken cancellationToken);
        Task DeletAsync(List<Guid> ids, CancellationToken cancellationToken);
        Task DeletAsync(List<TModel> models, CancellationToken cancellationToken);

        Task DeletPermanenteAsync(Guid id, CancellationToken cancellationToken);
        Task DeletPermanenteAsync(TModel model, CancellationToken cancellationToken);
        Task DeletPermanenteAsync(List<Guid> ids, CancellationToken cancellationToken);
        Task DeletPermanenteAsync(List<TModel> models, CancellationToken cancellationToken);

        #endregion

        Task<int> SaveChangeasync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
        void Rollback();
    }
}
