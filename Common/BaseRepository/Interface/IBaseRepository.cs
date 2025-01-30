using MyCar.Common.Model;
using MyCar.Context.Configurations;

namespace MyCar.Common.BaseRepository.Interface
{
    public interface IBaseRepository<TModel,TContext>
        where TModel : BaseModel
        where TContext : MyCarContext
    {
    }
}
