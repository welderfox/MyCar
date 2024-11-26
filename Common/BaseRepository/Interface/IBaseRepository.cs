using MyCar.Common.Model;

namespace MyCar.Common.BaseRepository.Interface
{
    public interface IBaseRepository<TModel,TContext>
        where TModel : BaseModel
        where TContext : DbContext
    {
    }
}
