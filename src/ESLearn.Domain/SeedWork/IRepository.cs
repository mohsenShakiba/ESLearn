using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESLearn.Domain.SeedWork
{
    public interface IRepository<T> where T: Entity, IAggregateRoot
    {

        Task IndexAsync(T document);
        Task IndexManyAsync(IEnumerable<T> documents);
        Task RemoveAsync(T document);
        Task RemoveManyAsync(IEnumerable<T> documents);
        Task<T> QueryAsync(string id);
        Task<IEnumerable<T>> SearchAsync(IElasticSearchQuery<T> query);

    }
}