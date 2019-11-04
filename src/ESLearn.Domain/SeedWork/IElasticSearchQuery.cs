using Nest;

namespace ESLearn.Domain.SeedWork
{
    public interface IElasticSearchQuery<T> where T: Entity
    {
        ISearchRequest<T> BuildSearchQuery(SearchDescriptor<T> descriptor);
    }
}