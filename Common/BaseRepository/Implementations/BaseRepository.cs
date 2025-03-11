using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyCar.Common.BaseRepository.Interface;
using MyCar.Common.Model;

namespace MyCar.Common.BaseRepository.Implementations
{

    public abstract class BaseRepository<TModel, TContext> : IBaseRepository<TModel, TContext>
            where TModel : BaseModel
            where TContext : DbContext
    {
        public BaseRepository(TContext context, ILogger<BaseRepository<TModel, TContext>> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected bool _disposed = false;
        protected readonly TContext _context;
        protected readonly ILogger<BaseRepository<TModel, TContext>> _logger;
        protected IDbContextTransaction _transaction;

        protected IQueryable<TModel> Query =>
            _context.Set<TModel>().Where(w => !w.DataExclusao.HasValue).AsQueryable();

        #region Metodos Get

        /// <summary>
        /// Retorna uma litas dos registros 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<TModel>> GetAsync(CancellationToken cancellationToken)
        {
            return await Query.Where(w => !w.IsDeleted.HasValue).ToListAsync();
        }

        /// <summary>
        /// Retorna um regitro de acordo com Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async virtual Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Query.SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retorna uma lista Ids informados
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<TModel>> GetAsync(List<Guid> ids, CancellationToken cancellationToken)
                => await Query.Where(w => ids.Contains(w.Id)).ToListAsync(cancellationToken);

        /// <summary>
        /// Retorna um booleano
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken)
                => await Query.AnyAsync(w => w.Id == id, cancellationToken);

        #endregion

        #region Metodos Insert

        public async virtual Task<TModel> InsertAsync(TModel model, CancellationToken cancellationToken)
        {
            await _context.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _context.Entry(model).State = EntityState.Detached;
            return await _context.Set<TModel>().FindAsync(new object[] { model.Id }, cancellationToken);
        }
        public async Task InsertAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            _context.AddRange(models);
            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Metodos Update

        public async Task UpdatetAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var model in models) model.Updat();
        }

        #endregion

        #region Metodos Delete/DeletePermanente

        public async virtual Task DeletAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _context.Set<TModel>().FindAsync(new object[] { id }, cancellationToken);
            if (model == null)
                throw new InvalidOperationException("Não existe registro para ser deletado.");

            await DeletAsync(model, cancellationToken);

        }
        public async virtual Task DeletAsync(TModel model, CancellationToken cancellationToken)
        {
            model.Delete();
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async virtual Task DeletAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var models = await _context.Set<TModel>().Where(w => ids.Contains(w.Id)).ToListAsync(cancellationToken);
            await DeletAsync(models, cancellationToken);
        }
        public async virtual Task DeletAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var model in models) model.Delete();
        }


        public async virtual Task DeletPermanenteAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _context.Set<TModel>().FindAsync(new object[] { id }, cancellationToken);
            if (model == null)
                throw new InvalidOperationException("Não existe registro para ser deletado.");
            await DeletPermanenteAsync(model, cancellationToken);
        }
        public async virtual Task DeletPermanenteAsync(TModel model, CancellationToken cancellationToken)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async virtual Task DeletPermanenteAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            var models = await _context.Set<TModel>().Where(w => ids.Contains(w.Id)).ToListAsync(cancellationToken);

            await DeletPermanenteAsync(models, cancellationToken);
        }
        public async virtual Task DeletPermanenteAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _context.RemoveRange(models);

            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion

        public async Task<int> SaveChangeasync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            if (_context.Database.CurrentTransaction != null)
            {
                _context.Database.CurrentTransaction.Commit();
                _transaction.Dispose();
            }
        }
        public void Rollback()
        {
            if (_context.Database.CurrentTransaction != null)
                _context.Database.CurrentTransaction.Rollback();
        }
    }
}
