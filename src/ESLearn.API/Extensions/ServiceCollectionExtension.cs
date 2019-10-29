using ESLearn.Repository.ElasticSearch;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ESLearn.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureElasticSearchContext<T>(this IServiceCollection collection, string url) where T: ElasticSearchContext, new()
        {
            var context = new T();
            context.SetupElasticClient(url);
            context.SetupConfiguration();
            collection.AddSingleton(_ => context.ElasticClient);
            collection.AddSingleton<IElasticSearchContext>(_ => context);
        } 
    }
}