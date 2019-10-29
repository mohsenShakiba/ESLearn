using System;
using System.Collections.Generic;
using ESLearn.Domain.AggregatesModel.UsersAggregate;
using ESLearn.Domain.SeedWork;
using Nest;

namespace ESLearn.Domain.AggregatesModel.PostsAggregate
{
    public class Post: Entity, IAggregateRoot
    {
        
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string[] Tags { get; set; }
        public long CreatedOn { get; set; }
        public long UpdatedOn { get; set; }
        
        [Nested]
        public IList<Comment> Comments { get; set; }

        public Post()
        {
        }

        public Post(User user, string title, string body)
        {
            Id = Guid.NewGuid().ToString();
            UserId = user.Id;
            Title = title;
            Body = body;
            Tags = new string[0];
            CreatedOn = DateTimeOffset.Now.ToUnixTimeSeconds();
            UpdatedOn = DateTimeOffset.Now.ToUnixTimeSeconds();
            Comments = new List<Comment>();
        }
    }
}