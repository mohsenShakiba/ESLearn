using ESLearn.Domain.AggregatesModel.PostsAggregate;
using ESLearn.Repository.Mappings;

namespace ESLearn.Repository.ElasticSearch
{
    public class AppContext: ElasticSearchContext
    {
        public override void SetupConfiguration()
        {
            Configure(new UserTypeMappingDescriptor());
            Configure(new PostTypeDescriptor(), new PostTypeDescriptor());
        }
    }
}