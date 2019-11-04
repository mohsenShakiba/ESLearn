using System.Threading.Tasks;
using ESLearn.Domain.AggregatesModel.PostsAggregate;
using ESLearn.Domain.SeedWork;
using Nest;

namespace ELLearn.Repository.Queries
{
    public class PostSearchQuery: IElasticSearchQuery<Post>
    {
        private readonly string _inputQueryStr;

        public PostSearchQuery(string inputQueryStr)
        {
            _inputQueryStr = inputQueryStr;
        }
        
        public ISearchRequest<Post> BuildSearchQuery(SearchDescriptor<Post> descriptor)
        {
            return descriptor.Query(q => q.Match(m => m.Field(f => f.Title).Query(_inputQueryStr).Analyzer("city_sysnonyms")));
        }
    }
}