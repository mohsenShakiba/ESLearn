using ESLearn.Domain.SeedWork;
using Nest;

namespace ESLearn.Repository.Mappings
{
    public interface IIndexAnalysisConfiguration<T>: IIndexConfiguration<T> where T: Entity
    {
        IAnalysis ConfigureAnalysis(AnalysisDescriptor analysisDescriptor);
    }
}