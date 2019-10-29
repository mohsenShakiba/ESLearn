using System;
using ESLearn.Domain.SeedWork;
using ESLearn.Repository.Mappings;
using Nest;

namespace ESLearn.Repository.ElasticSearch
{
    public interface IElasticSearchContext
    {
        void Configure<T>(ITypeMappingDescriptor<T> descriptor) where T : Entity;
        void ConfigureDefault<T>() where T : Entity;
    }
    
    public abstract class ElasticSearchContext: IElasticSearchContext
    {
        private IElasticClient _elasticClient;
        private ConnectionSettings _esConfiguration;

        public IElasticClient ElasticClient => _elasticClient;
        
        public void SetupElasticClient(string url)
        {
            Uri esInstance = new Uri(url);
            _esConfiguration = new ConnectionSettings(esInstance);
            _elasticClient = new ElasticClient(_esConfiguration);
        }

        public void Configure<T>(ITypeMappingDescriptor<T> descriptor) where T: Entity
        {
            _esConfiguration.DefaultMappingFor<T>(mappings => mappings.IndexName(descriptor.IndexName));
            _elasticClient.Indices.Create(descriptor.IndexName, (indexDescriptor) => indexDescriptor.Map<T>( 
                    mappingDescriptor => mappingDescriptor.Properties(descriptor.ConfigureMappings)
                )
            );
        }

        public void ConfigureDefault<T>() where T: Entity
        {
            var indexName = typeof(T).Name.ToLower();
            _esConfiguration.DefaultMappingFor<T>(mappings => mappings.IndexName(indexName).IdProperty(u => u.Id));
            _elasticClient.Indices.Create(indexName, (indexDescriptor) => indexDescriptor.Map<T>(m => m.AutoMap()));
        }

        public abstract void SetupConfiguration();

    }
}