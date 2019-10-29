using System;
using System.Collections.Generic;
using ESLearn.Domain.SeedWork;

namespace ESLearn.Domain.AggregatesModel.UsersAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string BioDescription { get; private set; }
        public long JoinDate { get; private set; }
        public IEnumerable<UserRoles> Roles { get; private set; }

        public User()
        {
        }

        public User(string userName, string password)
        {
            Id = Guid.NewGuid().ToString();
            UserName = userName;
            Password = password;
            BioDescription = "No description yet";
            JoinDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            Roles = new[] {UserRoles.User, UserRoles.Login};
        }

        public void UpdateBio(string bio)
        {
            BioDescription = bio;
        }

    }
}
    