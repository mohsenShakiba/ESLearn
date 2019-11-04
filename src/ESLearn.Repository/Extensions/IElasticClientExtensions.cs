using System.Threading.Tasks;
using ELLearn.Repository.Queries;
using ESLearn.Domain.SeedWork;
using Nest;

namespace ELLearn.Repository.Extensions
{
    public static class IElasticClientExtensions
    {
        public static Task<ISearchResponse<T>> SearchByQueryAsync<T>(this IElasticClient elasticClient, IElasticSearchQuery<T> query) where T: Entity
        {
            return elasticClient.SearchAsync<T>(query.BuildSearchQuery);
        }
    }
}