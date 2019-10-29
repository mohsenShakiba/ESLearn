using Nest;

namespace ESLearn.Repository.Mappings
{
    public interface ITypeMappingDescriptor<T> where T: class
    {
        string IndexName { get; }
        PropertiesDescriptor<T> ConfigureMappings(PropertiesDescriptor<T> descriptor);
    }
}