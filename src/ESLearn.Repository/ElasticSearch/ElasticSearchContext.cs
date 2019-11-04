using System;
using ESLearn.Domain.SeedWork;
using ESLearn.Repository.Mappings;
using Nest;

namespace ESLearn.Repository.ElasticSearch
{
    public interface IElasticSearchContext
    {
        void Configure<T>(ITypeMappingDescriptor<T> mappingDescriptor, IConfigureIndexDescriptor<T> indexDescriptor = null) where T : Entity;
        void ConfigureDefault<T>(IConfigureIndexDescriptor<T> indexDescriptor = null) where T : Entity;
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

        public void ConfigureSettings<T>() where T: Entity
        {
            
        }

        public void Configure<T>(ITypeMappingDescriptor<T> mappingDescriptor, IConfigureIndexDescriptor<T> indexDescriptor = null) where T: Entity
        {
            _esConfiguration.DefaultMappingFor<T>(mappings => mappings.IndexName(mappingDescriptor.IndexName));
            _elasticClient.Indices.Create(mappingDescriptor.IndexName, (id) => 
                id.Settings(sd => indexDescriptor?.ConfigureIndex(sd) ?? sd).Map<T>( 
                    md => md.Properties(mappingDescriptor.ConfigureMappings)
                )
            );
        }

        public void ConfigureDefault<T>(IConfigureIndexDescriptor<T> indexDescriptor = null) where T: Entity
        {
            var indexName = typeof(T).Name.ToLower();
            _esConfiguration.DefaultMappingFor<T>(mappings => mappings.IndexName(indexName).IdProperty(u => u.Id));
            _elasticClient.Indices.Create(indexName, (id) => id.Settings(sd => indexDescriptor?.ConfigureIndex(sd) ?? sd).Map<T>(m => m.AutoMap()));
        }

        public abstract void SetupConfiguration();

    }
}