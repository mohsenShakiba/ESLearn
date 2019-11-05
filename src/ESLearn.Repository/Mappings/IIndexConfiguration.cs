using ESLearn.Domain.SeedWork;

namespace ESLearn.Repository.Mappings
{
    public interface IIndexConfiguration<T> where T: Entity
    {
        string IndexName { get; }
    }
}