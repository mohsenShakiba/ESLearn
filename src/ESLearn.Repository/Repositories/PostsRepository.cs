using System.Collections.Generic;
using System.Threading.Tasks;
using ELLearn.Repository.Extensions;
using ESLearn.Domain.AggregatesModel.PostsAggregate;
using ESLearn.Domain.SeedWork;
using Nest;

namespace ESLearn.Repository.Repositories
{
    public class PostsRepository: IPostsRepository
    {
        private readonly IElasticClient _elasticClient;

        public PostsRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        
        public Task IndexAsync(Post document)
        {
            return _elasticClient.IndexDocumentAsync(document);
        }

        public Task IndexManyAsync(IEnumerable<Post> documents)
        {
            return _elasticClient.IndexManyAsync(documents);
        }

        public Task RemoveAsync(Post document)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveManyAsync(IEnumerable<Post> documents)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Post> QueryAsync(string id)
        {
            var result = await _elasticClient.GetAsync<Post>(id);
            return result.Source;
        }

        public async Task<IEnumerable<Post>> SearchAsync(IElasticSearchQuery<Post> query)
        {
            var results = await _elasticClient.SearchByQueryAsync(query);
            return results.Documents;
        }

        public Task AddCommentAsync(Post post, Comment comment)
        {
            post.Comments.Add(comment);
            return _elasticClient.UpdateAsync<Post, object>(new DocumentPath<Post>(post), u => u
                .Doc(new {post.Comments}));
        }
    }
}