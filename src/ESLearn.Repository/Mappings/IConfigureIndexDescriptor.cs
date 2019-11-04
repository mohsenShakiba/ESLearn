using ESLearn.Domain.SeedWork;
using Nest;

namespace ESLearn.Repository.Mappings
{
    public interface IConfigureIndexDescriptor<T> where T: Entity
    {
        IndexSettingsDescriptor ConfigureIndex(IndexSettingsDescriptor indexDescriptor);
    }
}