using System.Threading.Tasks;
using ESLearn.Domain.SeedWork;

namespace ESLearn.Domain.AggregatesModel.PostsAggregate
{
    public interface IPostsRepository: IRepository<Post>
    {
        Task AddCommentAsync(Post post, Comment comment);
    }
}