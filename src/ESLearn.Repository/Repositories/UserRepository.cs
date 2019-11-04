
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELLearn.Repository.Extensions;
using ELLearn.Repository.Queries;
using ESLearn.Domain.AggregatesModel.PostsAggregate;
using ESLearn.Domain.AggregatesModel.UsersAggregate;
using ESLearn.Domain.SeedWork;
using Nest;

namespace ESLearn.Repository.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly IElasticClient _elasticClient;

        public UserRepository(IElasticClient elasticClient)
        {
            
            _elasticClient = elasticClient;
        }
        
        public async Task IndexAsync(User document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            await _elasticClient.IndexDocumentAsync(document);
        }

        public async Task IndexManyAsync(IEnumerable<User> documents)
        {
            if (!documents.Any())
                return;
            await _elasticClient.IndexManyAsync(documents);
        }

        public Task RemoveAsync(User document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            throw new System.NotImplementedException();
        }

        public Task RemoveManyAsync(IEnumerable<User> documents)
        {
            if (!documents.Any())
                return Task.CompletedTask;
            throw new System.NotImplementedException();
        }

        public async Task<User> QueryAsync(string id)
        {
            var results = await _elasticClient.GetAsync<User>(id);
            return results.Source;
        }

        public async Task<IEnumerable<User>> SearchAsync(IElasticSearchQuery<User> query)
        {
            var results = await _elasticClient.SearchByQueryAsync(query);
            return results.Documents;
        }
    }
}