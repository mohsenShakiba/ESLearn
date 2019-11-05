using System;
using ESLearn.Domain.SeedWork;
using ESLearn.Repository.Mappings;
using Nest;

namespace ESLearn.Repository.ElasticSearch
{
    public interface IElasticSearchContext
    {
        void Configure<T>(IIndexConfiguration<T> configuration) where T : Entity;
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

        public void ConfigureSettings<T>() where T: Entity
        {
            
        }

        public void Configure<T>(IIndexConfiguration<T> configuration) where T: Entity
        {
            var analysisConfiguration = (configuration as IIndexAnalysisConfiguration<T>);
            var mappingConfiguration = (configuration as IIndexMappingsConfiguration<T>);
            _esConfiguration.DefaultMappingFor<T>(mappings => mappings.IndexName(configuration.IndexName));
            _elasticClient.Indices.Create(configuration.IndexName, (id) => 
                id.Settings(sd => sd.Analysis(ad => analysisConfiguration?.ConfigureAnalysis(ad) ?? ad)).Map<T>( 
                    md => md.Properties(pd => mappingConfiguration?.ConfigureMapping(pd) ?? pd)
                )
            );
        }

        public void ConfigureDefault<T>() where T: Entity
        {
            var indexName = typeof(T).Name.ToLower();
            _esConfiguration.DefaultMappingFor<T>(mappings => mappings.IndexName(indexName).IdProperty(u => u.Id));
            _elasticClient.Indices.Create(indexName, (id) => id.Map<T>(m => m.AutoMap()));
        }

        public abstract void SetupConfiguration();

    }
}