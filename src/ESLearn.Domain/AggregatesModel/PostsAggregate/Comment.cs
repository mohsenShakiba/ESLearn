using System;
using ESLearn.Domain.SeedWork;

namespace ESLearn.Domain.AggregatesModel.PostsAggregate
{
    public class Comment: Entity, IAggregateRoot
    {

        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public long CreatedAt { get; set; }

        public Comment()
        {
            
        }

        public Comment(Post post, string message)
        {
            Id = Guid.NewGuid().ToString();
            PostId = post.Id;
            UserId = post.UserId;
            Message = message;
            CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}