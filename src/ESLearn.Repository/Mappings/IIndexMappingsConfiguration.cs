using ESLearn.Domain.SeedWork;
using Nest;

namespace ESLearn.Repository.Mappings
{
    public interface IIndexMappingsConfiguration<T>: IIndexConfiguration<T> where T: Entity
    {
        PropertiesDescriptor<T> ConfigureMapping(PropertiesDescriptor<T> descriptor);

    }
    
    
}